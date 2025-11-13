namespace Frontend.Models
{
    public class ClienteViewModel
    {
        public int Id { get; set; }

        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        public DateTime Fecha_Nacimiento { get; set; }

        public string Cuit { get; set; }

        public string Domicilio { get; set; }

        public string Telefono { get; set; }

        public string Email { get; set; }
    }
}
