using System.Collections.Generic;

namespace MercadoPago.Resources
{
    public class DiscountCampaign : MPBase
    {
        #region Actions

        public static DiscountCampaign Find(decimal transactionAmount, string payerEmail)
        {
            return Find(transactionAmount, payerEmail, null, WITHOUT_CACHE, null);
        }

        public static DiscountCampaign Find(decimal transactionAmount, string payerEmail, bool useCache, MPRequestOptions requestOptions)
        {
            return Find(transactionAmount, payerEmail, null, useCache, requestOptions);
        }

        public static DiscountCampaign Find(decimal transactionAmount, string payerEmail, string couponCode)
        {
            return Find(transactionAmount, payerEmail, couponCode, WITHOUT_CACHE, null);
        }

        [GETEndpoint("/v1/discount_campaigns")]
        public static DiscountCampaign Find(decimal transactionAmount, string payerEmail, string couponCode, bool useCache, MPRequestOptions requestOptions)
        {
            var queryParams = new Dictionary<string, string>();
            queryParams.Add("transaction_amount", transactionAmount.ToString());
            queryParams.Add("payer_email", payerEmail);
            queryParams.Add("coupon_code", couponCode);
            
            return ProcessMethod<DiscountCampaign>(typeof(DiscountCampaign), null, "Find", queryParams, useCache, requestOptions);
        }

        #endregion

        #region Properties

        private string _id;
        private string _name;
        private decimal? _percentOff;
        private decimal? _amountOff;
        private decimal? _couponAmount;
        private string _currencyId;

        #endregion

        #region Accessors

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public decimal? PercentOff
        {
            get { return _percentOff; }
            set { _percentOff = value; }
        }

        public decimal? AmountOff
        {
            get { return _amountOff; }
            set { _amountOff = value; }
        }

        public decimal? CouponAmount
        {
            get { return _couponAmount; }
            set { _couponAmount = value; }
        }

        public string CurrencyId
        {
            get { return _currencyId; }
            set { _currencyId = value; }
        }

        #endregion
    }
}