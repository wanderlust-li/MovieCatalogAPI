// виводимо по жанрам


import axios from 'axios';
import React, { useEffect, useState } from 'react';
import { MdChevronLeft, MdChevronRight } from 'react-icons/md';

const Row = () => {
    const [movies, setMovies] = useState([]);

    useEffect(() => {
        const axiosWithCors = axios.create({
            baseURL: 'http://localhost:5222/api/v1',
            headers: {
                'Content-Type': 'application/json',
            },
        });

        axiosWithCors.get('/Movie')
            .then(response => {
                console.log(response.data); // Will log the movie array from the API response
                setMovies(response.data.result); // Set the movie array in the state
            })
            .catch(error => {
                console.error('error API:', error);
            });
    }, []);

    return (
        <div>
            {movies.map(movie => (
                <div key={movie.id}>
                    <h2 className='text-white font-bold md:text-xl p-4'>{movie.title}</h2>
                    <div className='relative flex items-center group'>
                        <MdChevronLeft
                            // onClick={slideLeft} // You need to define the slideLeft function
                            className='bg-white left-0 rounded-full absolute opacity-50 hover:opacity-100 cursor-pointer z-10 hidden group-hover:block'
                            size={40}
                        />
                        <div
                            className='w-full h-full overflow-x-scroll whitespace-nowrap scroll-smooth scrollbar-hide relative'
                        >
                            {movie.rating}
                        </div>
                        {/* <MdChevronRight
                            onClick={slideRight} // You need to define the slideRight function or remove this line
                            className='bg-white right-0 rounded-full absolute opacity-50 hover:opacity-100 cursor-pointer z-10 hidden group-hover:block'
                            size={40}
                        /> */}
                    </div>
                </div>
            ))}
        </div>
    );
};

export default Row;
