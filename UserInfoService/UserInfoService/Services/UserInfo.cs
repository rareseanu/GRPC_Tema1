﻿using Google.Protobuf.WellKnownTypes;
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
                return "Masculin";
            } else
            {
                return "Feminin";
            }
        }

        public string GetVarsta(string CNP)
        {
            int anNastere = Int32.Parse(CNP.Substring(1, 2));
            return (DateTime.Now.Year - anNastere).ToString();
        }
        
        public bool IsValid(string CNP)
        {
            if(CNP.Length == 13)
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