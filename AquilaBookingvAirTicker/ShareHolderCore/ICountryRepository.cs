using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShareHolderCore.Domain.Model;

namespace ShareHolderCore.Domain
{
    public interface ICountryRepository
    {
        Country getCountryById(short? it);
    }
}
