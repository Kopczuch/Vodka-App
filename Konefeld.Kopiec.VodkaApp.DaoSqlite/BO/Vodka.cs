using System.ComponentModel.DataAnnotations.Schema;
using Konefeld.Kopiec.VodkaApp.Core;
using Konefeld.Kopiec.VodkaApp.Interfaces;

namespace Konefeld.Kopiec.VodkaApp.DaoSqlite.BO
{
    public class Vodka : IVodka
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public VodkaType Type { get; set; }
        public double AlcoholPercentage { get; set; }
        public double VolumeInLiters { get; set; }
        public double Price { get; set; }
        public string? FlavourProfile { get; set; }


        public int ProducerId { get; set; }

        [NotMapped]
        public IProducer Producer
        {
            get => ProducerImpl;
            set
            {
                ProducerImpl = (Producer)value;
                ProducerId = ProducerImpl.Id;
            }
        }

        public Producer ProducerImpl { get; set; }

    }
}
