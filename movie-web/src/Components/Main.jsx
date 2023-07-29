import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { Link } from 'react-router-dom';
import '../style/Main.css'

const Main = () => {
    const [movies, setMovies] = useState([]);

    useEffect(() => {
        const fetchMovies = async () => {
            try {
                const response = await axios.get('http://localhost:5222/api/v1/Movie');
                setMovies(response.data.result);
            } catch (error) {
                console.error('Error fetching movies:', error);
            }
        };
        fetchMovies();
    }, []);

    return (
        <div className="movies-grid">
            {movies.map(movie => (
                <Link to={`/movie/${movie.id}`} key={movie.id} className="movie-card">
                    <img
                        className="movie-image"
                        src={movie.imageUrl}
                        alt={movie.title}
                    />
                    <div className="movie-details">
                        <h2 className="movie-title">{movie.title}</h2>
                        <p className="movie-description">{movie.description}</p>
                        <div className="movie-rating">
                            <span className="rating-label">Rating:</span>
                            <span className="rating-value">{movie.rating}</span>
                        </div>
                    </div>
                </Link>
            ))}
        </div>
    );
};

export default Main;
