using System;
using System.Collections.Generic;
using System.Text;
using Entities.Models.Basic;

namespace Entities.Models.Basic
{
    public class ResultModel : BaseModel
    {
        private bool v1;
        private string v2;

        public ResultModel()
        {

        }

        public ResultModel(bool result)
        {
            this.result = result ? 1 : 0;
            this.message = result ? "تراکنش با موفقیت انجام شد" : "خطا در انجام تراکنش";
        }

        public ResultModel(bool result, string Message)
        {
            this.result = result ? 1 : 0;
            this.message = Message;
        }
        public int result { get; set; }
        public int? resultId { get; set; }
        public string message { get; set; }

    }

    public class ResultModel<T>
    {
        public bool result { get; set; }
        public string message { get; set; }
        public T value { get; set; }
        public ResultModel()
        {

        }
        public ResultModel(T value)
        {
            result = true;
            this.value = value;
            this.message = "تراکنش با موفقیت بود";
        }

        public ResultModel(Exception ex)
        {
            result = false;
            this.message = ex.Message;
        }
    }
}
