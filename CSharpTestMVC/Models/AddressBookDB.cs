using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;


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
                GetUserByUserID(1, ref currUser);
                HttpContext.Current.Session["currUserID"] = currUser.ID;
            }
            else
            {
                int currUserID = (int)HttpContext.Current.Session["currUserID"];
                GetUserByUserID(currUserID,ref currUser);
                HttpContext.Current.Session["currUserID"] = currUserID;
            }
            return currUser;
        }

        public static UInt32 CreateNewUser(string userName, string firstName, string lastName, bool isAdmin)
        {
            UInt32 retVal = 0;
            User newUser = new User();
            newUser.userName = userName;
            newUser.firstName = firstName;
            newUser.lastName = lastName;
            newUser.isAdmin = isAdmin;
            var db = new IDCCanadaAddressBook();
            db.Users.Add(newUser);
            db.SaveChanges();
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
        //Contact Code
        public static UInt32 CreateNewContact(string firstName, string lastName, string phoneNumber, string streetName, string city, string province, string postalCode, string country)
        {
            UInt32 retVal = 0;
            User currUser = GetCurrentUser();
            Contact newContact = new Contact();
            var db = new IDCCanadaAddressBook();
            newContact.firstName = firstName;
            newContact.lastName = lastName;
            newContact.phoneNumber = phoneNumber;
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
    }
}