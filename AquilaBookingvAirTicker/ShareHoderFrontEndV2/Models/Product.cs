using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShareHolderCore.Domain.Model;
using ShareHolderCore;
using ShareHolderCore.Domain.Repositories;

namespace ShareHoderFrontEndV2.Models
{
    public class Product
    {
        public Int64 Id { get; set; }
        public Availability Availability { get; set; }
        public int NoRoom { get; set; }

       

        public Product(Int64 id, int NoRoom)
        {
            this.Id = id;
            this.NoRoom = NoRoom;
            IAvailabilityRepository<Availability> Availabilityrp = new AvailabilityRepository();
            Availability Availability = Availabilityrp.getAvailabilityById(id);

            this.Availability = Availability;
            
        }  
    }
}