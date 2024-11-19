using System.Net;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;

namespace API.Controllers;

public class DoctorController : BaseApiController
{
    private readonly IDoctorService _doctorService;
    private ApiResponse _response;
    
    public DoctorController(IDoctorService doctorService)
    {
        _doctorService = doctorService;
        _response = new();
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            _response.Result = await _doctorService.GetAll();
            _response.isSuccessfull = true;
            _response.StatusCode = HttpStatusCode.OK;
        }
        catch (Exception e)
        {
            _response.isSuccessfull = false;
            _response.Message = e.Message;
            _response.StatusCode = HttpStatusCode.BadRequest;
        }

        return Ok(_response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(DoctorDto modelDto)
    {
        try
        {
            await _doctorService.Add(modelDto);
            _response.isSuccessfull = true;
            _response.StatusCode = HttpStatusCode.Created;
        }
        catch (Exception e)
        {
            _response.isSuccessfull = false;
            _response.Message = e.Message;
            _response.StatusCode = HttpStatusCode.BadRequest;
        }

        return Ok(_response);
    }

    [HttpPut]
    public async Task<IActionResult> Edit(DoctorDto modelDto)
    {
        try
        {
            await _doctorService.Update(modelDto);
            _response.isSuccessfull = true;
            _response.StatusCode = HttpStatusCode.NoContent;
        }
        catch (Exception e)
        {
            _response.isSuccessfull = false;
            _response.Message = e.Message;
            _response.StatusCode = HttpStatusCode.BadRequest;
        }

        return Ok(_response);
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _doctorService.Remove(id);
            _response.isSuccessfull = true;
            _response.StatusCode = HttpStatusCode.NoContent;
        }
        catch (Exception e)
        {
            _response.isSuccessfull = false;
            _response.Message = e.Message;
            _response.StatusCode = HttpStatusCode.BadRequest;
        }

        return Ok(_response);
    }

}