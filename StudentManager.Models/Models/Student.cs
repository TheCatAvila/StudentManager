﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManager.Models.Models
{
    public class Student
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string SecondLastName { get; set; }
        public string Email { get; set; }
        public int Age {  get; set; }
        public Grade Grade { get; set; }
        public Boolean? State {  get; set; }  
        public string FullName => $"{FirstName} {LastName}";
        public string StateText => State.HasValue ? (State.Value ? "Activo" : "Inactivo") : "Estado Desconocido";
    }
}
