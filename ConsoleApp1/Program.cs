using System;
using System.Text.Json;
using Common.DTO.AuthDTO;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var token = new Token
            {
                RefreshToken = "awdawdawdawd",
                ExpiresIn = 21284124,
                AccessToken = "aw89d718f12d21=d12-0h8 d1298"
            };
            var json = JsonSerializer.Serialize(new { token = token });
            Console.WriteLine(json);
        }
    }
}