using TMDT.Data;
using TMDT.Models;

namespace TMDT.Services
{
    public class VoucherService : IVoucherService
    {
        private readonly TMDTDbContext _context;

        public VoucherService(TMDTDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Voucher> GetAvailableVouchers()
        {
            return _context.Voucher.Where(v => !v.IsUsed).ToList();
        }

        public VoucherValidationResult ValidateVoucher(int voucherId)
        {
            var voucher = _context.Voucher.SingleOrDefault(v => v.Id == voucherId);

            if (voucher != null && !voucher.IsUsed)
            {
                voucher.IsUsed = true;
                _context.SaveChanges();

                return new VoucherValidationResult
                {
                    IsValid = true,
                    Discount = voucher.Discount
                };
            }

            return new VoucherValidationResult
            {
                IsValid = false,
                Discount = 0
            };
        }

        public VoucherValidationResult ValidateVoucher(string voucherCode)
        {
            throw new NotImplementedException();
        }
    }

}
