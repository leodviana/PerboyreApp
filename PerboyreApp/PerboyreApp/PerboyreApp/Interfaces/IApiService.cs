﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PerboyreApp.Models;

namespace PerboyreApp.Interfaces
{
    public interface IApiService
    {
        Task<List<paciente>> getPacientes(long Id_dentista);
        Task<List<Dentista>> getDentistas();
        Task<List<ArqImagens>> getExames(paciente pac);
        Task<List<ArqImagens>> getExamespdf(paciente pac);
        Task<Response> Login(string email, string password);
        Task<Response> PutDentista(Dentista dentista);
        Task<byte[]> getExame(paciente pac);
    }
}
