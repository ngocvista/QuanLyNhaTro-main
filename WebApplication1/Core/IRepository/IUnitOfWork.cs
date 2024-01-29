﻿using MotelManagement.Business.IService;
using MotelManagement.Data.Models;

namespace MotelManagement.Core.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        // Inteface IRepository
        IRoomTypeRepository roomTypeRepository { get; }
        IUserRepository userRepository { get; }
        IRoomRepository roomRepository { get; }

        IBookingRepository bookingRepository { get; }
        IPassingRepository passingRepository { get; }
        IContractRepository contractRepository { get; } 

        IBillRepository billRepository { get; }
        IImageRespository imageRespository { get; }
        IArticleRespository articleRespository { get; }

        void save();

        Task SaveAsync();
    }
}