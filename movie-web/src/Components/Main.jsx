import React, { useEffect, useState } from 'react';
import axios from 'axios';

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
        <div className="grid grid-cols-2 sm:grid-cols-3 md:grid-cols-4 gap-4">
            {movies.map(movie => (
                <div key={movie.id} className="bg-white rounded-lg shadow-md p-4">
                    <img
                        className="w-full h-40 object-cover rounded-t-lg"
                        src={movie.imageUrl}
                        alt={movie.title}
                    />
                    <div className="p-4">
                        <h2 className="text-lg font-bold mb-2">{movie.title}</h2>
                        <p className="text-gray-700 text-sm h-20 overflow-hidden">{movie.description}</p>
                        <div className="flex items-center mt-4">
                            <span className="mr-2">Rating:</span>
                            <span>{movie.rating}</span>
                        </div>
                    </div>
                </div>
            ))}
        </div>
    );
};

export default Main;
