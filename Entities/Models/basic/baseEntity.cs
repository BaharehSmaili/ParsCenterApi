using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.basic
{
    public interface IEntity
    {
        //جهت معرفی مدل ها به انتیتی فریمورک و دی بی کانتکس 
        //اگر مدل ازین اینترفیس ایمپلیمنت بشه یعنی به دی بی کانتکس معرفی شده
    }

    public abstract class baseEntity<TKey> : IEntity
    {
        public TKey id { get; set; }
    }

    public abstract class baseEntity : baseEntity<Guid>
    {

    }
}
