using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;
using Vidly.Dtos;
using AutoMapper;
using System.Data.Entity;

namespace Vidly.Controllers.API
{
    public class MoviesController : ApiController
    {
        //create our context
        private ApplicationDbContext _context;

		public MoviesController()
		{
            _context = new ApplicationDbContext();
		}

		protected override void Dispose(bool disposing)
		{
			_context.Dispose();
		}

		// GET /api/movies
		public /*IEnumerable<MovieDto>*/ IHttpActionResult GetMovies(string query = null)
		{
			var movieDtos = _context.Movies
				.Include(m => m.Genre)
				.ToList()
				.Select(Mapper.Map<Movie, MovieDto>); //Something about a delegate here and we're not actually invoking the method. Look it up. LS - 68

			return Ok(movieDtos);
		}

		// GET /api/movies/1
		public /*MovieDto*/ IHttpActionResult GetMovie(int id)
		{
			//This would be like "Edit", so we need to fetch the movie from the Db first...
			var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

			if(movie == null)
			{
				//throw new HttpResponseException(HttpStatusCode.NotFound);
				return NotFound();
			}

			return Ok(Mapper.Map<Movie, MovieDto>(movie));
		}

		// POST /api/movies
		[Authorize(Roles = RoleName.CanManageMovies)]
		[HttpPost]
		public /*MovieDto*/ IHttpActionResult CreateMovie(MovieDto movieDto)
		{
			if (!ModelState.IsValid)
			{
				//throw new HttpResponseException(HttpStatusCode.BadRequest);
				return BadRequest();
			}

			var movie = Mapper.Map<MovieDto, Movie>(movieDto);

			_context.Movies.Add(movie);
			_context.SaveChanges();

			movieDto.Id = movie.Id;

			return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto); ;
		}

		// PUT /api/movies/1
		[Authorize(Roles = RoleName.CanManageMovies)]
		[HttpPut]
		public void UpdateMovie(int id, MovieDto movieDto)
		{
			//check ModelState
			if (!ModelState.IsValid)
			{
				throw new HttpResponseException(HttpStatusCode.BadRequest);
			}

			//fetch movie to be updated from database
			var movieInDb = _context.Movies.Single(m => m.Id == id);

			if(movieInDb == null)
			{
				throw new HttpResponseException(HttpStatusCode.NotFound);
			}

			Mapper.Map<MovieDto, Movie>(movieDto, movieInDb);

			_context.SaveChanges();

		}

		[Authorize(Roles = RoleName.CanManageMovies)]
		[HttpDelete]
		public void DeleteMovie(int id)
		{
			var movie = _context.Movies.Single(c => c.Id == id);

			if(movie == null)
			{
				throw new HttpResponseException(HttpStatusCode.NotFound);
			}

			_context.Movies.Remove(movie);

			_context.SaveChanges();
		}
	}
}
