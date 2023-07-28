import React from "react";
import { Route, Routes } from "react-router-dom";
import Navbar from '../src/Components/Navbar';
import Home from '../src/pages/Home'

function App() {
    return (
        <div>
            <Navbar />
            <div style={{ paddingTop: '100px' }}> {/* Increase the paddingTop value for a bigger gap */}
                <Home />
            </div>
        </div>
    );
}

export default App;
