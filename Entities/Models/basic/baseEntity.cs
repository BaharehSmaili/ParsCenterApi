using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.Basic
{
    public interface IEntity
    {
        //جهت معرفی مدل ها به انتیتی فریمورک و دی بی کانتکس 
        //اگر مدل ازین اینترفیس ایمپلیمنت بشه یعنی به دی بی کانتکس معرفی شده
    }

    public abstract class BaseEntity<TKey> : IEntity
    {
        public TKey Id { get; set; }
    }

    public abstract class BaseEntity : BaseEntity<Guid>
    {

    }
}
