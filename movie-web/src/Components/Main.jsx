import React, { useEffect, useState } from 'react';
import axios from 'axios';

const MovieList = () => {
    const [movies, setMovies] = useState([]);

    useEffect(() => {
        // Здійснення запиту до вашого API
        axios.get('http://localhost:3000/api/v1/Movie')
            .then(response => {
                // Встановлення даних з API в стан компонента
                setMovies(response.data);
            })
            .catch(error => {
                console.error('Помилка при отриманні даних з API:', error);
            });
    }, []);

    return (
        <div>
            {/* Відображення даних */}
            {movies.map(movie => (
                <div>
                    <h2>{movie.Title}</h2>
                    <p>{movie.Description}</p>
                    {/* Додайте інші дані, які хочете відобразити */}
                </div>
            ))}
        </div>
    );
};

export default MovieList;
