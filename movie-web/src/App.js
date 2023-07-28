import { Route, Routes } from "react-router-dom";
import Navbar from '../src/Components/Navbar';
import Home from '../src/pages/Home'

function App() {
    return (
        <div>
            <Navbar/>
            <Home/>
            {/* <Routes>
                <Route path='/' element={<Home/>} />
            </Routes> */}
        </div>
    );
}

export default App;
