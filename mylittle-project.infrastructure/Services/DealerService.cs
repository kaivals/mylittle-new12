﻿using System;
using System.Threading.Tasks;
using mylittle_project.Application.DTOs;
using mylittle_project.Application.Interfaces;
using mylittle_project.Domain.Entities;
using mylittle_project.infrastructure.Data;

namespace mylittle_project.infrastructure.Services
{
    public class DealerService : IDealerService
    {
        private readonly AppDbContext _context;

        public DealerService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateBusinessInfoAsync(DealerDto dto)
        {
            var dealerUser = new UserDealer
            {
                Username = dto.BusinessEmail,
                Role = "Dealer",
                IsActive = true
            };
            _context.UserDealers.Add(dealerUser);

            var Dealer = new Dealer
            {
                TenantId = dto.TenantId,
                DealerName = dto.DealerName,
                BusinessName = dto.BusinessName,
                BusinessNumber = dto.BusinessNumber,
                BusinessEmail = dto.BusinessEmail,
                BusinessAddress = dto.BusinessAddress,
                ContactEmail = dto.ContactEmail,
                PhoneNumber = dto.PhoneNumber,
                Website = dto.Website,
                TaxId = dto.TaxIdOrGstNumber,
                Country = dto.Country,
                State = dto.State,
                City = dto.City,
                Timezone = dto.Timezone,
                UserDealer = dealerUser
            };

            _context.Dealers.Add(Dealer);
            await _context.SaveChangesAsync();

            // Auto-generate Virtual Number
            var virtualNumber = "VN" + DateTime.UtcNow.Ticks.ToString().Substring(5, 10);

            var virtualAssignment = new VirtualNumberAssignment
            {
                DealerId = Dealer.Id, // Updated property name
                VirtualNumber = virtualNumber
            };

            _context.VirtualNumberAssignments.Add(virtualAssignment);
            await _context.SaveChangesAsync();

            return Dealer.Id;
        }
    }
}
