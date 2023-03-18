using System.ComponentModel.DataAnnotations;

namespace WebAPIpractice.Models
{
    public class estados_reserva
    {
        [Key]

        public int estado_res_id { get; set; }

        public string estados { get; set;}
    }
}
