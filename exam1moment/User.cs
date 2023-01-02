using System;

namespace Exam1Moment
{
    public static class User
    {
        public static string Email;
        public static string Password;

        public static void AutorizeUser()
        {
            while (true)
            {
                Console.WriteLine("Введите электронную почту: ");
                string email = Console.ReadLine();
                if (EmailIsValid(email))
                    Email = email;
                else
                {
                    Console.WriteLine("Некорректный электронный адрес. Повторите ввод: ");
                    continue;
                }

                Console.WriteLine("Введите пароль: ");
                string password = Console.ReadLine();
                Password = password;
                return;
            }
            
        }

        public static bool EmailIsValid(string email)
        {
            var trimmedEmail = email.Trim();
            if (trimmedEmail.EndsWith(".")) {
                return false;
            }
            try {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch {
                return false;
            }
        }
    }
}