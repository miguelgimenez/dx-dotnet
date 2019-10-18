﻿using MercadoPago.DataStructures.Payment;
using MercadoPago.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using MercadoPago.DataStructures.MerchantOrder;
using Newtonsoft.Json.Converters;
using Payer = MercadoPago.DataStructures.Payment.Payer;

namespace MercadoPago.Resources
{

    /// <summary>
    /// This service allows you to create, modify or read payments
    /// </summary>
    public class Payment : MPBase
    {
        #region Actions

        /// <summary>
        /// Find a payment trought an unique identifier
        /// </summary>
        public static Payment FindById(long? id)
        {
            return FindById(id, WITHOUT_CACHE, null);
        }

        /// <summary>
        /// Find a payment trought an unique identifier with Local Cache Flag
        /// </summary>
        [GETEndpoint("/v1/payments/:id")]
        public static Payment FindById(long? id, bool useCache, MPRequestOptions requestOptions)
        {
            return (Payment)ProcessMethod<Payment>(typeof(Payment), "FindById", id.ToString(), useCache, requestOptions);
        }

        public Boolean Save()
        {
            return ProcessMethodBool<Payment>("Save", WITHOUT_CACHE, null);
        }

        /// <summary>
        /// Save a new payment
        /// </summary>
        [POSTEndpoint("/v1/payments")]
        public Boolean Save(MPRequestOptions requestOptions)
        {
            return ProcessMethodBool<Payment>("Save", WITHOUT_CACHE, requestOptions);
        }

        public Boolean Update()
        {
            return ProcessMethodBool<Payment>("Update", WITHOUT_CACHE, null);
        }

        /// <summary>
        /// Update editable properties
        /// </summary>
        [PUTEndpoint("/v1/payments/:id")]
        public Boolean Update(MPRequestOptions requestOptions)
        {
            return ProcessMethodBool<Payment>("Update", WITHOUT_CACHE, requestOptions);
        }

        /// <summary>
        /// Get all payments
        /// </summary>
        public static List<Payment> All()
        {
            return All(WITHOUT_CACHE, null);
        }

        /// <summary>
        /// Get all payments acoording to specific filters
        /// </summary>
        public static List<Payment> Search(Dictionary<string, string> filters)
        {
            return Search(filters, WITHOUT_CACHE, null);
        }

        /// <summary>
        /// Get all payments, with using cache option
        /// </summary>
        [GETEndpoint("/v1/payments/search")]
        public static List<Payment> All(bool useCache, MPRequestOptions requestOptions)
        {
            return (List<Payment>)ProcessMethodBulk<Payment>(typeof(Payment), "Search", useCache, requestOptions);
        }

        /// <summary>
        /// Get all payments acoording to specific filters, with using cache option
        /// </summary>
        [GETEndpoint("/v1/payments/search")]
        public static List<Payment> Search(Dictionary<string, string> filters, bool useCache, MPRequestOptions requestOptions)
        {
            return (List<Payment>)ProcessMethodBulk<Payment>(typeof(Payment), "Search", filters, useCache, requestOptions);
        }
        #endregion

        #region Interactions
        /// <summary>
        /// Payment refund
        /// </summary> 
        public Payment Refund()
        {
            return Refund(null, null);
        }

        /// <summary>
        /// Payment refund
        /// </summary> 
        public Payment Refund(MPRequestOptions requestOptions)
        {
            return Refund(null, requestOptions);
        }

        /// <summary>
        /// Partial payment refund
        /// </summary> 
        public Payment Refund(decimal amount)
        {
            return Refund(amount, null);
        }

        /// <summary>
        /// Partial payment refund
        /// </summary> 
        public Payment Refund(decimal? amount, MPRequestOptions requestOptions)
        {
            Refund refund = new Refund();
            refund.manualSetPaymentId((decimal)_id);
            refund.Amount = amount;
            refund.Save(requestOptions);

            if (refund.Id.HasValue)
            {
                Thread.Sleep(500);
                var payment = Payment.FindById(_id, WITHOUT_CACHE, requestOptions);
                _status = payment.Status;
                _status_detail = payment.StatusDetail;
                _transaction_amount_refunded = payment.TransactionAmountRefunded;
                _refunds = payment.Refunds;
            }
            else
            {
                _errors = refund.Errors;
            }

            return this;
        }
        #endregion

        #region Properties 
        private long? _id;
        private DateTime? _date_created;
        private DateTime? _date_approved;
        private DateTime? _date_last_updated;
        private DateTime? _money_release_date;
        private int? _collector_id; 
        [JsonConverter(typeof(StringEnumConverter))]
        private OperationType? _operation_type;
        private Payer _payer;
        private Collector _collector;
        private bool? _binary_mode;
        private bool? _live_mode;
        private Order? _order ;
        private string _external_reference;
        private string _description;
        private string _marketplace;
        private JObject _metadata;
        [StringLength(3)] 
        [JsonConverter(typeof(StringEnumConverter))]
        private CurrencyId? _currency_id;
        private decimal? _transaction_amount;
        private decimal? _net_amount;
        private decimal? _transaction_amount_refunded;
        private decimal? _coupon_amount;
        private int? _campaign_id;
        private string _coupon_code;
        private TransactionDetail? _transaction_details;
        private List<FeeDetail> _fee_details;
        private int? _differential_pricing_id;
        private decimal? _application_fee;  
        [JsonConverter(typeof(StringEnumConverter))]
        private PaymentStatus? _status;         
        private string _status_detail;
        private bool? _capture ;
        private bool? _captured;
        private string _call_for_authorize_id;
        private string _payment_method_id;
        private string _issuer_id; 
        [JsonConverter(typeof(StringEnumConverter))]
        private PaymentTypeId? _payment_type_id;        
        private string _token ;
        private DataStructures.Payment.Card? _card;
        private string _statement_descriptor;
        private int? _installments ;
        private string _notification_url;
        private string _callback_url;
        private List<Refund> _refunds;
        private AdditionalInfo? _additional_info; 
        private string _processing_mode;
        private string _merchant_account_id;
        private DateTime? _date_of_expiration;
        private long? _sponsor_id;
        private List<Taxes> _taxes;
        #endregion

        #region Accessors 
        /// <summary>
        /// Payment identifier
        /// </summary>
        public long? Id
        {
            get { return this._id; }
            private set { this._id = value; }
        } 
        /// <summary>
        /// Payment’s creation date
        /// </summary>
        public DateTime? DateCreated 
        {
            get { return this._date_created; }
            private set { this._date_created = value; }
        } 
        /// <summary>
        /// Payment’s approval date
        /// </summary>
        public DateTime? DateApproved
        {
            get { return this._date_approved; }
            private set { this._date_approved = value; }
        } 
        /// <summary>
        /// Last modified date
        /// </summary>
        public DateTime? DateLastUpdated
        {
            get { return this._date_last_updated; }
            private set { this._date_last_updated = value; }
        } 
        /// <summary>
        /// Release date of payment
        /// </summary>
        public DateTime? MoneyReleaseDate
        {
            get { return this._money_release_date; }
            private set { this._money_release_date = value; }
        } 
        /// <summary>
        /// Identifies the seller
        /// </summary>
        public int? CollectorId
        {
            get { return this._collector_id; }
            private set { this._collector_id = value; }
        } 
        /// <summary>
        /// Payment type
        /// </summary>
        public OperationType? OperationType 
        {
            get { return this._operation_type; }
            set { this._operation_type = value; }
        } 
        /// <summary>
        /// Identifies the buyer  
        /// </summary>
        public Payer Payer 
        {
            get { return this._payer; }
            set { this._payer = value; }
        }

        public Collector Collector
        {
            get { return _collector; }
            set { _collector = value; }
        }
        /// <summary>
        /// When set to true, the payment can only be approved or rejected. 
        /// Otherwise in_process status is added
        /// </summary>
        public bool? BinaryMode
        {
            get { return this._binary_mode; }
            set { this._binary_mode = value; }
        } 
        /// <summary>
        /// Whether the payment will be processed in sandbox or in production mode
        /// </summary>
        public bool? LiveMode
        {
            get { return this._live_mode; }
            private set { this._live_mode = value; }
        } 
        /// <summary>
        /// Order identifier
        /// </summary>
        public Order? Order
        {
            get { return this._order; }
            set { this._order = value; }
        } 
        /// <summary>
        /// ID given by the merchant in their system
        /// </summary>
        public string ExternalReference
        {
            get { return this._external_reference; }
            set { this._external_reference = value; }
        }
        /// <summary>
        /// Payment reason or item title
        /// </summary>
        public string Description
        {
            get { return this._description; }
            set { this._description = value; }
        }
        public string Marketplace
        {
            get { return this._marketplace; }
            set { this._marketplace = value; }
        }
        /// <summary>
        /// Valid JSON that can be attached to the payment to record additional attributes of the merchant
        /// </summary>
        public JObject Metadata
        {
            get { return this._metadata; }
            set { this._metadata = value; }
        } 
        /// <summary>
        /// ID of the currency used in the payment
        /// </summary>
        public CurrencyId? CurrencyId
        {
            get { return this._currency_id; }
            private set { this._currency_id = value; }
        } 
        /// <summary>
        /// Product cost
        /// </summary>
        public decimal? TransactionAmount
        {
            get { return this._transaction_amount; }
            set { this._transaction_amount = value; }
        }

        /// <summary>
        /// Product net
        /// </summary>
        public decimal? NetAmount
        {
            get { return this._net_amount; }
            set { this._net_amount = value; }
        } 
        /// <summary>
        /// Total refunded amount in this payment
        /// </summary>
        public decimal? TransactionAmountRefunded
        {
            get { return this._transaction_amount_refunded; }
            private set { this._transaction_amount_refunded = value; }
        } 
        /// <summary>
        /// Amount of the coupon discount
        /// </summary>
        public decimal? CouponAmount
        {
            get { return this._coupon_amount; }
            set { this._coupon_amount = value; }
        } 
        /// <summary>
        /// Discount campaign ID
        /// </summary>
        public int? CampaignId
        {
            private get { return this._campaign_id; }
            set { this._campaign_id = value; }
        } 
        /// <summary>
        /// Discount campaign with a specific code
        /// </summary>
        public string CouponCode
        {
            private get { return this._coupon_code; }
            set { this._coupon_code = value; }
        } 
        /// <summary>
        /// Groups the details of the transaction
        /// </summary>
        public TransactionDetail? TransactionDetails
        {
            get { return this._transaction_details; }
            private set { this._transaction_details = value; }
        } 
        /// <summary>
        /// List of fees
        /// </summary>
        public List<FeeDetail> FeeDetails
        {
            get { return this._fee_details; }
            private set { this._fee_details = value; }
        } 
        /// <summary>
        /// Id of the scheme for the absorption of financing fee
        /// </summary>
        public int? DifferentialPricingId
        {
            get { return this._differential_pricing_id; }
            set { this._differential_pricing_id = value; }
        } 
        /// <summary>
        /// Fee collected by a marketplace or MercadoPago Application
        /// </summary>
        public decimal? ApplicationFee
        {
            private get { return this._application_fee; }
            set { this._application_fee = value; }
        } 
        /// <summary>
        /// Payment status
        /// </summary>
        public PaymentStatus? Status
        {
            get { return this._status; }
            set { this._status = value; }
        } 
        /// <summary>
        /// Gives more detailed information on the current state or rejection cause
        /// </summary>
        public string StatusDetail
        {
            get { return this._status_detail; }
            private set { this._status_detail = value; }
        } 
        /// <summary>
        /// Gives more detailed information on the current state or rejection cause
        /// </summary>
        public bool? Capture
        {
            private get { return this._capture; }
            set { this._capture = value; }
        } 
        /// <summary>
        /// Gives more detailed information on the current state or rejection cause
        /// </summary>
        public bool? Captured
        {
            get { return this._captured; }
            private set { this._captured = value; }
        } 
        /// <summary>
        /// Gives more detailed information on the current state or rejection cause
        /// </summary>
        public string CallForAuthorizeId
        {
            get { return this._call_for_authorize_id; }
            private set { this._call_for_authorize_id = value; }
        } 
        /// <summary>
        /// Gives more detailed information on the current state or rejection cause
        /// </summary>
        public string PaymentMethodId
        {
            get { return this._payment_method_id; }
            set { this._payment_method_id = value; }
        } 
        /// <summary>
        /// Payment method issuer
        /// </summary>
        public string IssuerId
        {
            get { return this._issuer_id; }
            set { this._issuer_id = value; }
        } 
        /// <summary>
        /// Type of payment method chosen
        /// </summary>
        public PaymentTypeId? PaymentTypeId
        {
            get { return this._payment_type_id; }
            set { this._payment_type_id = value; }
        } 
        /// <summary>
        /// Card token ID
        /// </summary>
        public string Token
        {
            private get { return this._token; }
            set { this._token = value; }
        } 
        /// <summary>
        /// Details of the card used
        /// </summary>
        public DataStructures.Payment.Card? Card
        {
            get { return this._card; }
            private set { this._card = value; }
        } 
        /// <summary>
        /// How will look the payment in the card bill (e.g.: MERCADOPAGO)
        /// </summary>
        public string StatementDescriptor
        {
            get { return this._statement_descriptor; }
            set { this._statement_descriptor = value; }
        } 
        /// <summary>
        /// Selected quantity of installments
        /// </summary>
        public int? Installments
        {
            get { return this._installments; }
            set { this._installments = value; }
        } 
        /// <summary>
        /// URL where mercadopago will send notifications associated to changes in this payment
        /// </summary>
        public string NotificationUrl
        {
            get { return this._notification_url; }
            set { this._notification_url = value; }
        } 
        /// <summary>
        /// URL where mercadopago does the final redirect (only for bank transfers)
        /// </summary>
        public string CallbackUrl
        {
            get { return this._callback_url; }
            set { this._callback_url = value; }
        } 
        /// <summary>
        /// List of refunds that were made to this payment
        /// </summary>
        public List<Refund> Refunds
        {
            get { return this._refunds; }
            private set { this._refunds = value; }
        } 
        /// <summary>
        /// Data that could improve fraud analysis and conversion rates. 
        /// Try to send as much information as possible.
        /// </summary>
        public AdditionalInfo? AdditionalInfo
        {
            private get { return this._additional_info; }
            set { this._additional_info = value; }
        }
        /// <summary>
        /// Processing mode to define if an specific merchannt id should be used.
        /// </summary>
        public string ProcessingMode
        {
            get { return this._processing_mode; }
            set { this._processing_mode = value; }
        }
        /// <summary>
        /// Merchant Id for complex payment cases
        /// </summary>
        public string MerchantAccountId
        {
            get { return _merchant_account_id; }
            set { _merchant_account_id = value; }
        }

        public DateTime? DateOfExpiration
        {
            get { return _date_of_expiration; } 
            set { _date_of_expiration = value; }
        }

        /// <summary>
        /// Sponsor Identification
        /// </summary>
        public long? SponsorId
        {
            get { return _sponsor_id; } 
            set { _sponsor_id = value; }
        }

        /// <summary>
        /// Taxes for payments
        /// </summary>
        public List<Taxes> Taxes
        {
            get { return this._taxes; }
            set { this._taxes = value; }
        }
        #endregion

    }
}
