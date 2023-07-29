import React from "react";
import '../index.css';

const Navbar = () => {
    return (
        <div
            className='relative'
            style={{
                height: '50vh', // Stretch to half of the screen
                overflow: 'hidden', // Hide content that overflows the block
                backgroundImage: `url("https://images.unsplash.com/photo-1554941426-a965fb2b9258?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8NjV8fGNpbmVtYXxlbnwwfHwwfHx8MA%3D%3D&auto=format&fit=crop&w=500&q=60")`, // Background image
                backgroundSize: 'contain', // Scale down the image to fit entirely without cropping
                backgroundRepeat: 'no-repeat', // Disable image repetition
                backgroundPosition: 'center', // Center the image within the block
            }}
        >
            <div className='flex items-center justify-between p-4 z-[100] w-full absolute'>
                <h1 className='text-white text-4xl font-bold cursor-pointer'>
                    Wanderlust
                </h1>
                <div className='flex items-center space-x-4'>
                    <button className='text-white pr-4'>Sign In</button>
                    <button className='bg-blue-600 px-6 py-2 rounded cursor-pointer text-white'>Sign Up</button>
                </div>
            </div>
        </div>
    )
}

export default Navbar;
