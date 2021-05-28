using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Interfaces
{
  public interface IGetAllWithPageBaseQuery<out T>: IRequest<T>
    {
          int PageNumber { get; set; }
          int PageSize { get; set; }
    }
}
