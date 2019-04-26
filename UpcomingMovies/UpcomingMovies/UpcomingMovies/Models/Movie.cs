using System;
using System.Collections.Generic;
using System.Text;

namespace UpcomingMovies.Models
{
    public class Movie
    {
        public int Id { get; internal set; }
        public string Title { get; internal set; } // Nome
        public string Poster_path { get; internal set; } // imagem do pôster // concatenar com Get API Configuration
        public List<int>Genre_ids { get; internal set; } // ID Genero LISTA
        public string Backdrop_path { get; internal set; } // imagem de fundo
        public string Overview { get; internal set; } // Overview - Visão Geral
        public string Release_date { get; internal set; } // Data de Lançamento

        public List<Genre> Genres { get; internal set; }

        public string Poster_path_Img { get; internal set; }
        public string Backdrop_path_Img { get; internal set; }
    }
}
