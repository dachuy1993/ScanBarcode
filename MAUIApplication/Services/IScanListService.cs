using MAUIApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIApplication.Services
{
    public interface IScanListService
    {
        Task AddScanList(string barcodeSS, string barcode);
        Task<IEnumerable<TblScanList>> GetScanLists();
        Task<TblScanList> GetScanList(int id);

    }
}
