import React from "react";
import '../index.css'

const Navbar = () => {
    return (
        <div className='flex items-center justify-between p-4 z-[100] w-full absolute'>
            <h1 className='text-blue-600 text-4xl font-bold cursor-pointer'>
                Wanderlust
            </h1>
            <div>
                <button className='text-white pr-4'>Sign In</button>
                <button className='bg-blue-600 px-6 py-2 rounded cursor-pointer text-white'>Sign Up</button>
            </div>
        </div>
    )
}

export default Navbar;