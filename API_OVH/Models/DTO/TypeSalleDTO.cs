﻿namespace API_OVH.Models.DTO
{
    public class TypeSalleDTO
    {
        private int idTypeSalle;
        private string nomTypeSalle;

        public int IdTypeSalle { get => idTypeSalle; set => idTypeSalle = value; }
        public string NomTypeSalle { get => nomTypeSalle; set => nomTypeSalle = value; }

        public override bool Equals(object? obj)
        {
            return obj is TypeSalleDTO dTO &&
                   this.IdTypeSalle == dTO.IdTypeSalle &&
                   this.NomTypeSalle == dTO.NomTypeSalle;
        }
    }
}
