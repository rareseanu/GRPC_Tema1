using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserInfoService.Services
{
    public class InfoService : UserInfo.UserInfoBase
    {
        public string GetGen(string CNP)
        {
            if(CNP[0] == '2' || CNP[0] == '6')
            {
                return "Feminin";
            } else
            {
                return "Masculin";
            }
        }

        public string GetVarsta(string CNP)
        {
            int anNastere = Int32.Parse(CNP.Substring(1, 2));
            if(anNastere > DateTime.Now.Year)
            {
                return (DateTime.Now.Year - anNastere - 1900).ToString();
            } else
            {
                return (DateTime.Now.Year - anNastere - 2000).ToString();
            }
        }

        public bool IsDigitsOnly(string CNP)
        {
            foreach (char c in CNP)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
        
        public bool IsValid(string CNP)
        {
            if(CNP.Length == 13 && IsDigitsOnly(CNP))
            {
                return true;
            }
            return false;
        }

        public override Task<UserInfoReply> GetUserInfo(UserInfoRequest request, ServerCallContext context)
        {
            var CNP = request.CNP;
            var nume = request.Nume;
            string varsta, gen;
            if(IsValid(CNP))
            {
                Console.WriteLine($"[ {nume} ] CNP valid.");
                gen = GetGen(CNP);
                varsta = GetVarsta(CNP);
                Console.WriteLine($"[ {nume} ] Varsta: {varsta} Gen: {gen}.");
                return Task.FromResult(new UserInfoReply() { Nume = nume, Gen = gen, Varsta = varsta });
            } else
            {
                Console.WriteLine($"[ {nume} ] CNP invalid.");
                return Task.FromResult(new UserInfoReply() { Nume=nume, Gen="CNP_Invalid", Varsta="CNP_Invalid" });
            }
        }
    }
}
