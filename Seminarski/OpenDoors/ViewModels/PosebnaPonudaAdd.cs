﻿using OpenDoors.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenDoors.ViewModels
{
    public class PosebnaPonudaAdd
    {
        public string Opis { get; set; }
        public string Popust { get; set; }
        public int NekretninaId { get; set; }
    }
}
