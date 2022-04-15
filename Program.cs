// See https://aka.ms/new-console-template for more information
using System;
using System.Threading;
using System.Collections.Generic;

namespace Potok
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Введите текст  для отправки. Для выхода напишите /exit");

            var command = Console.ReadLine();

            while (command != "/exit")
            {
                SendRequest(command);
                command = Console.ReadLine();
            }

            Console.WriteLine("Завершение работы");
        }

        public static void SendRequest(string message)
        {
            Console.WriteLine("Будет отправлено сообщение '{0}'", message);
            Console.WriteLine("Введите аргументы сообщения. Если аргументы не нужны - введите /end");

            var arguments = new List<string>();
            string argument = Console.ReadLine();

            while (argument != "/end")
            {
                arguments.Add(argument);
                Console.WriteLine("Введите следующий аргумент сообщения. Для окончания добавления аргументов введите /end");
                argument = Console.ReadLine();
            }

            var id = Guid.NewGuid().ToString("D");
            Console.WriteLine("Было отправлено сообщение '{0}'. Присвоен идентификатор {1}", message, id);
            ThreadPool.QueueUserWorkItem(callBack => HandleRequest(message, arguments.ToArray(), id));
        }

        public static void HandleRequest(string message, string[] arguments, string id)
        {
            try
            {
                var requestHandler = new DummyRequestHandler();
                var response = requestHandler.HandleRequest(message, arguments);
                Console.WriteLine("Сообщение с идентификатором {0} получило ответ - {1}", id, response);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Сообщение с идентификатором {0} получило ответ - {1}", id, ex.Message);
            }
        }
    }

    public interface IRequestHandler
    {
        /// <summary>
        /// Обработать запрос.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <param name="arguments">Аргументы запроса.</param>
        /// <returns>Результат выполнения запроса.</returns>
        string HandleRequest(string message, string[] arguments);
    }


    /// <summary>
    /// Тестовый обработчик запросов.
    /// </summary>
    public class DummyRequestHandler : IRequestHandler
    {
        /// <inheritdoc />
        public string HandleRequest(string message, string[] arguments)
        {
            // Притворяемся, что делаем что то.
            Thread.Sleep(10_000);
            if (message.Contains("упади"))
            {
                throw new Exception("Я упал, как сам просил");
            }
            return Guid.NewGuid().ToString("D");
        }
    }
}



