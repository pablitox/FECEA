// See https://aka.ms/new-console-template for more information
using Ntec2.Redis;

Console.WriteLine("Prueba de REDIS");

RedisController controller = new RedisController();

controller.Del("prueba");



Console.ReadKey();