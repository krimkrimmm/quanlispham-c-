using TMDT.Models;

namespace TMDT.Services
{
    public interface IVoucherService
    {
        VoucherValidationResult ValidateVoucher(int voucherId);
        IEnumerable<Voucher> GetAvailableVouchers();
    }
}
