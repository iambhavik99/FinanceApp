using FinanceApp.Application.Interfaces;
using FinanceApp.Infrastructure.Interfaces;
using FinanceApp.Infrastructure.Models.Users;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserResponseMedia> SignUp(UserRequestMedia userRequestMedia)
        {
            return await _userRepository.SignUp(userRequestMedia);
        }

        public async Task<UserResponseMedia> login(UserLoginRequestMedia userLoginRequestMedia, string aesKeyString, string ivString)
        {
            var user = await _userRepository.login(userLoginRequestMedia);
            if (user != null && authenticate(userLoginRequestMedia.password, user.password, aesKeyString, ivString))
            {
                return new UserResponseMedia()
                {
                    id = user.id,
                    email = user.email,
                    username = user.username
                };
            }
            return null;
        }

        public bool authenticate(string password, string hashPassword, string aesKeyString, string ivString)
        {

            byte[] aesKey = Convert.FromBase64String(aesKeyString);
            byte[] iv = Convert.FromBase64String(ivString);

            return decrypt(password, aesKey, iv) == decrypt(hashPassword, aesKey, iv);

        }




        public string decrypt(string encryptedString, byte[] aesKey, byte[] iv)
        {
            using (var aes = new AesGcm(aesKey))
            {
                byte[] encryptedBytes = Convert.FromBase64String(encryptedString);

                int length = encryptedBytes.Length;
                int offset = Math.Max(0, length - AesGcm.TagByteSizes.MaxSize);

                byte[] encryptedDataString = new byte[offset];

                byte[] tag = new byte[length - offset];

                Array.Copy(encryptedBytes, 0, encryptedDataString, 0, offset);
                Array.Copy(encryptedBytes, offset, tag, 0, length - offset);

                var plaintextBytes = new byte[encryptedDataString.Length];

                aes.Decrypt(iv, encryptedDataString, tag, plaintextBytes);
                var result = Encoding.UTF8.GetString(plaintextBytes);
                return result;
            }
        }



    }
}
