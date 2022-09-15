using System.ComponentModel.DataAnnotations;

namespace ProgWeb3APIEventos.Core.Model
{
    public class EventReservation
    {
        public long IdReservation { get; set; }
        [Required(ErrorMessage = "É preciso informar o Id do evento que se deseja fazer a reserva.")]
        public long IdEvent { get; set; }
        [Required(ErrorMessage = "A reserva deve possuir um titular.")]
        public string PersonName { get; set; }
        [Required(ErrorMessage = "É preciso informar no mínimo uma pessoa para a reserva.")]
        public long Quantity { get; set; }
    }
}
