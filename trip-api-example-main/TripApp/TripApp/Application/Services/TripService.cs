﻿using TripApp.Application.DTOs;
using TripApp.Application.Mappers;
using TripApp.Application.Repository;
using TripApp.Application.Services.Interfaces;
using TripApp.Core.Models;

namespace TripApp.Application.Services;

public class TripService : ITripService
{
    private readonly ITripRepository _tripRepository;

    public TripService(ITripRepository tripRepository)
    {
        _tripRepository = tripRepository;
    }

    public async Task<PaginatedResult<GetTripDto>> GetPaginatedTripsAsync(int page = 1, int pageSize = 10)
    {
        if (page < 1) page = 1;
        if (pageSize < 10) page = 10;
        var result = await _tripRepository.GetPaginatedTripsAsync(page, pageSize);

        var mappedTrips = new PaginatedResult<GetTripDto>
        {
            AllPages = result.AllPages,
            Data = result.Data.Select(trip => trip.MapToGetTripDto()).ToList(),
            PageNum = result.PageNum,
            PageSize = result.PageSize
        };

        return mappedTrips;
    }

    public async Task<List<GetTripDto>> GetAllTripsAsync()
    {
        var trips = await _tripRepository.GetAllTripsAsync();
        var mappedTrips = trips.Select(trip => trip.MapToGetTripDto()).ToList();
        return mappedTrips;
    }
}