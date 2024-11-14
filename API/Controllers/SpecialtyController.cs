using System.Net;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;

namespace API.Controllers;

public class SpecialtyController : BaseApiController
{
    private readonly ISpecialtyService _specialtyService;
    private ApiResponse _response;
    
    public SpecialtyController(ISpecialtyService specialtyService)
    {
        _specialtyService = specialtyService;
        _response = new();
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            _response.Result = await _specialtyService.GetAll();
            _response.isSuccessful = true;
            _response.StatusCode = HttpStatusCode.OK;
        }
        catch (Exception e)
        {
            _response.isSuccessful = false;
            _response.Message = e.Message;
            _response.StatusCode = HttpStatusCode.BadRequest;
        }

        return Ok(_response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(SpecialtyDto modelDto)
    {
        try
        {
            await _specialtyService.Add(modelDto);
            _response.isSuccessful = true;
            _response.StatusCode = HttpStatusCode.Created;
        }
        catch (Exception e)
        {
            _response.isSuccessful = false;
            _response.Message = e.Message;
            _response.StatusCode = HttpStatusCode.BadRequest;
        }

        return Ok(_response);
    }

    [HttpPut]
    public async Task<IActionResult> Edit(SpecialtyDto modelDto)
    {
        try
        {
            await _specialtyService.Update(modelDto);
            _response.isSuccessful = true;
            _response.StatusCode = HttpStatusCode.NoContent;
        }
        catch (Exception e)
        {
            _response.isSuccessful = false;
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
            await _specialtyService.Remove(id);
            _response.isSuccessful = true;
            _response.StatusCode = HttpStatusCode.NoContent;
        }
        catch (Exception e)
        {
            _response.isSuccessful = false;
            _response.Message = e.Message;
            _response.StatusCode = HttpStatusCode.BadRequest;
        }

        return Ok(_response);
    }

}