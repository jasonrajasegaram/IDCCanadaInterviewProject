using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;
using System.Net.Mail;
using System.Net;


namespace CSharpTestMVC.Models
{
    public class AddressBookDB
    {
        //Encryption Code
        public static UInt32 encrypt(string textToEncrypt, ref string encryptedString)
        {
            UInt32 retVal = 0;
            try
            {
                AesManaged myAes = new AesManaged();
                myAes.KeySize = 256;
                myAes.BlockSize = 128;
                myAes.Mode = CipherMode.CBC;
                string keyStr = "AAECAwQFBgcICQoLDA0ODw==";
                string ivStr = "AAECAwQFBgcICQoLDA0ODw==";
                byte[] keyArr = Convert.FromBase64String(keyStr);
                byte[] ivArr = Convert.FromBase64String(ivStr);
                myAes.Key = keyArr;
                myAes.IV = ivArr;
                byte[] plainText = ASCIIEncoding.UTF8.GetBytes(textToEncrypt);
                ICryptoTransform crypto = myAes.CreateEncryptor();

                byte[] cipherText = crypto.TransformFinalBlock(plainText, 0, plainText.Length);
                encryptedString = Convert.ToBase64String(cipherText);

            }
            catch (Exception e)
            {

            }
            return retVal;
        }
        public static UInt32 decrypt(string textToDecrypt, ref string decryptedString)
        {
            UInt32 retVal = 0;
            try
            {
                AesManaged myAes = new AesManaged();
                myAes.KeySize = 256;
                myAes.BlockSize = 128;
                myAes.Mode = CipherMode.CBC;
                string keyStr = "AAECAwQFBgcICQoLDA0ODw==";
                string ivStr = "AAECAwQFBgcICQoLDA0ODw==";
                byte[] keyArr = Convert.FromBase64String(keyStr);
                byte[] ivArr = Convert.FromBase64String(ivStr);
                myAes.Key = keyArr;
                myAes.IV = ivArr;
                byte[] plainText = Convert.FromBase64String(textToDecrypt);

                ICryptoTransform decrypto = myAes.CreateDecryptor();

                byte[] decipherText = decrypto.TransformFinalBlock(plainText, 0, plainText.Length);
                decryptedString = ASCIIEncoding.UTF8.GetString(decipherText);

            }
            catch (Exception e)
            {

            }
            return retVal;
        }
        //User Code
        public static UInt32 SetSessionUser(User user)
        {
            UInt32 retVal = 0;
            HttpContext.Current.Session["currUserID"] = user.ID;
            return retVal;
        }
        public static User GetCurrentUser()
        {
            User currUser = new User();
            var currUserIDCheck = HttpContext.Current.Session["currUserID"];
            var db = new IDCCanadaAddressBook();
            if (currUserIDCheck == null)
            {
                return null;
            }
            else
            {
                int currUserID = (int)HttpContext.Current.Session["currUserID"];
                GetUserByUserID(currUserID,ref currUser);
                HttpContext.Current.Session["currUserID"] = currUserID;
            }
            return currUser;
        }

        public static UInt32 AddNewUser(string userName, string firstName, string lastName, bool isAdmin, ref bool userExists)
        {
            UInt32 retVal = 0;
            User newUser = new User();
            var db = new IDCCanadaAddressBook();
            newUser = db.Users.Where(u => u.userName == userName).FirstOrDefault();
            if (newUser == null)
            {
                newUser = new User();
                string encryptedFirstname = "";
                string encryptedLastname = "";
                encrypt(firstName, ref encryptedFirstname);
                encrypt(lastName, ref encryptedLastname);
                newUser.userName = userName;
                newUser.firstName = encryptedFirstname;
                newUser.lastName = encryptedLastname;
                newUser.isAdmin = isAdmin;
                string passwordToEncrypt = System.Web.Security.Membership.GeneratePassword(9,0);
                string encryptedPassword = "";
                encrypt(passwordToEncrypt, ref encryptedPassword);
                newUser.password = encryptedPassword;
                newUser.temporaryPassword = encryptedPassword;

                db.Users.Add(newUser);
                db.SaveChanges();
                Uri myuri = new Uri(HttpContext.Current.Request.Url.AbsoluteUri);
                string pathQuery = myuri.PathAndQuery;
                string hostName = myuri.ToString().Replace(pathQuery, "");
                string forgotPasswordLink = hostName + "/ForgotPassword/Index";
                SendEmail(newUser.userName, "Address Book Account Confirmation", "Hello " + firstName + ", Your username is " + userName + " and your temporary password is " + passwordToEncrypt + ". To change your password please go to the following link: " + forgotPasswordLink);
            }
            else
            {
                userExists = true;
            }
            return retVal;
        }

        public static UInt32 GetUserByUserID(int userid, ref User user)
        {
            UInt32 retVal = 0;
            var db = new IDCCanadaAddressBook();
            user = db.Users.Where(u => u.ID == userid).FirstOrDefault();
            return retVal;
        }
        public static UInt32 GetUserByUsername(string username, ref User user)
        {
            UInt32 retVal = 0;
            var db = new IDCCanadaAddressBook();
            user = db.Users.Where(u => u.userName == username).FirstOrDefault();
            return retVal;
        }
        public static UInt32 DeleteUser(int userid)
        {
            UInt32 retVal = 0;
            User user = new User();
            var db = new IDCCanadaAddressBook();
            user = db.Users.Where(u => u.ID == userid).FirstOrDefault();
            if (user != null)
            {
                List<Contact> contacts = user.userContacts.ToList();

                if(contacts!= null && contacts.Count >0){
                    foreach (Contact aContact in contacts)
                    {
                        db.Contacts.Remove(aContact);
                    }
                }
                db.Users.Remove(user);
                db.SaveChanges();
            }
            return retVal;
        }
        public static UInt32 EditUser(int userid, string firstName, string lastName, string userName, bool? isAdmin)
        {
            UInt32 retVal = 0;
            User user = new User();
            var db = new IDCCanadaAddressBook();
            user = db.Users.Where(u => u.ID == userid).FirstOrDefault();
            if (user != null)
            {
                bool isAdminCheck = false;
                if (isAdmin.HasValue)
                {
                    isAdminCheck = isAdmin.Value;
                }
                else
                {
                    isAdminCheck = user.isAdmin;
                }
                string encryptedFirstName = "";
                string encryptedLastName = "";
                encrypt(firstName, ref encryptedFirstName);
                encrypt(lastName, ref encryptedLastName);
                user.firstName = encryptedFirstName;
                user.lastName = encryptedLastName;
                user.userName = userName;
                user.isAdmin = isAdminCheck;
                db.SaveChanges();
            }
            return retVal;
        }
        //Contact Code
        public static UInt32 CreateNewContact(string firstName, string lastName, string phoneNumber, string streetName, string city, string province, string postalCode, string country)
        {
            UInt32 retVal = 0;
            User currUser = GetCurrentUser();
            Contact newContact = new Contact();
            var db = new IDCCanadaAddressBook();
            string encryptedFirstName = "";
            string encryptedLastName = "";
            string encryptedPhoneNumber = "";
            encrypt(firstName, ref encryptedFirstName);
            encrypt(lastName, ref encryptedLastName);
            encrypt(phoneNumber, ref encryptedPhoneNumber);
            newContact.firstName = encryptedFirstName;
            newContact.lastName = encryptedLastName;
            newContact.phoneNumber = encryptedPhoneNumber;
            newContact.streetName = streetName;
            newContact.city = city;
            newContact.province = province;
            newContact.postalCode = postalCode;
            newContact.country = country;
            newContact.contact_connection = db.Users.Where(u=>u.ID == currUser.ID).FirstOrDefault();
            db.Contacts.Add(newContact);
            currUser.userContacts.Add(newContact);
            db.SaveChanges();
            return retVal;
        }
        public static UInt32 DeleteContact(int contactID)
        {
            UInt32 retVal = 0;
            var db = new IDCCanadaAddressBook();
            Contact aContact = new Contact();
            aContact= db.Contacts.Where(c=>c.ID==contactID).FirstOrDefault();
            if (aContact != null)
            {
                db.Contacts.Remove(aContact);
                db.SaveChanges();
            }
            return retVal;
        }
        public static UInt32 EditContact(int contactID, string firstName, string lastName, string phoneNumber, string streetName, string city, string province, string postalCode, string country)
        {
            UInt32 retVal = 0;
            var db = new IDCCanadaAddressBook();
            Contact aContact = new Contact();
            aContact = db.Contacts.Where(c => c.ID == contactID).FirstOrDefault();
            string encryptedFirstName="";
            string encryptedLastName="";
            string encryptedPhoneNumber="";
            encrypt(firstName, ref encryptedFirstName);
            encrypt(lastName, ref encryptedLastName);
            encrypt(phoneNumber, ref encryptedPhoneNumber);
            if (aContact != null)
            {
                aContact.firstName = encryptedFirstName;
                aContact.lastName = encryptedLastName;
                aContact.phoneNumber = encryptedPhoneNumber;
                aContact.streetName = streetName;
                aContact.province = province;
                aContact.city = city;
                aContact.postalCode = postalCode;
                aContact.country = country;
                db.SaveChanges();
            }
            return retVal;
        }
        //Email Code
        public static UInt32 SendEmail(string toAddress, string subject, string body)
        {
            UInt32 retVal = 0;
            MailAddress to = new MailAddress(toAddress);
            MailAddress from = new MailAddress("no-reply@jmqtnonsense.ca");
            MailMessage mail = new MailMessage(from, to);
            NetworkCredential credentials = new NetworkCredential("no-reply@jmqtnonsense.ca", "ZMG59k7v");
            mail.Subject=subject;
            mail.Body=body;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "mail.jmqtnonsense.ca";
            smtp.Port = 25;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = credentials;
            smtp.Send(mail);

            return retVal;
        }
        //Password Code
        public static bool TemporaryPasswordCheck(string userName, string temporaryPassword)
        {
            bool tempCorrect = false;
            User user = new User();
            var db = new IDCCanadaAddressBook();
            user = db.Users.Where(u => u.userName == userName).FirstOrDefault();
            if (user != null)
            {
                if (user.temporaryPassword != null)
                {
                    string tempdecryptedPassword = "";
                    decrypt(user.temporaryPassword, ref tempdecryptedPassword);
                    if (tempdecryptedPassword == temporaryPassword)
                    {
                        tempCorrect = true;
                    }
                }
            }
            return tempCorrect;
        }
        public static UInt32 ChangePassword(string userName, string newPassword)
        {
            UInt32 retVal = 0;
            User user = new User();
            var db = new IDCCanadaAddressBook();
            user = db.Users.Where(u => u.userName == userName).FirstOrDefault();
            if (user != null)
            {
                string encryptedNewPassword = "";
                encrypt(newPassword, ref encryptedNewPassword);
                user.password = encryptedNewPassword;
                user.temporaryPassword = null;
                db.SaveChanges();
            }
            return retVal;
        }
        public static bool SendTemporaryPassword(string userName)
        {
            bool userFound = false;
            User user = new User();
            var db = new IDCCanadaAddressBook();
            user = db.Users.Where(u => u.userName == userName).FirstOrDefault();
            if (user != null)
            {
                userFound = true;
                string decryptedFirstName = "";
                decrypt(user.firstName, ref decryptedFirstName);
                string passwordToEncrypt = System.Web.Security.Membership.GeneratePassword(9, 0);
                string encryptedTempPassword = "";
                encrypt(passwordToEncrypt, ref encryptedTempPassword);
                user.temporaryPassword = encryptedTempPassword;
                db.SaveChanges();
                Uri myuri = new Uri(HttpContext.Current.Request.Url.AbsoluteUri);
                string pathQuery = myuri.PathAndQuery;
                string hostName = myuri.ToString().Replace(pathQuery, "");
                string forgotPasswordLink = hostName + "/ForgotPassword/Index";
                SendEmail(userName, "Address Book Account Confirmation", "Hello " + decryptedFirstName + ", Your username is " + userName + " and your temporary password is " + passwordToEncrypt + ". To change your password please go to the following link: " + forgotPasswordLink);
            }
            return userFound;
        }
        public static UInt32 ChangePasswordByUserID(int userID, string oldPassword, string newPassword, ref bool correctPassword)
        {
            UInt32 retVal = 0;
            User user = new User();
            var db = new IDCCanadaAddressBook();
            user = db.Users.Where(u => u.ID == userID).FirstOrDefault();
            if (user != null)
            {
                string decryptedPassword = "";
                decrypt(user.password, ref decryptedPassword);
                if (decryptedPassword == oldPassword)
                {
                    correctPassword = true;
                    string encryptedNewPassword = "";
                    encrypt(newPassword, ref encryptedNewPassword);
                    user.password = encryptedNewPassword;
                    db.SaveChanges();
                }
            }
            return retVal;
        }
    }
}