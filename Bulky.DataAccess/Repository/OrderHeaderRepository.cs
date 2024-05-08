﻿using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private ApplicationDbContext _db;
        public OrderHeaderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(OrderHeader obj)
        {
            _db.OrderHeaders.Update(obj);
        }

		public void UpdateStatus(int id, string orderStatus, string? paymentStatus = null)
		{
            var orderfromDb = _db.OrderHeaders.FirstOrDefault(u => u.Id == id);
            if(orderfromDb != null)
            {
                orderfromDb.OrderStatus=orderStatus;

                if(string.IsNullOrEmpty(paymentStatus)) 
                {
                orderfromDb.PaymentStatus=paymentStatus;
                }
            }
		}

		public void UpdateStripePaymentID(int id, string sessionId, string paymentIntentId)
		{
			var orderfromDb = _db.OrderHeaders.FirstOrDefault(u => u.Id == id);
            if (!string.IsNullOrEmpty(sessionId))

			{
                orderfromDb.SessionId = sessionId;
            }
			if (!string.IsNullOrEmpty(paymentIntentId))

			{
				orderfromDb.SessionId = paymentIntentId;
                orderfromDb.PaymentDate= DateTime.Now;
			}
		}
	}
}
