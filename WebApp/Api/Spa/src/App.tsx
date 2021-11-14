import { createTheme, ThemeProvider } from '@mui/material/styles';
import React from 'react';
import { AddGameForm } from './AddGameForm';
import './App.css';

const darkTheme = createTheme({
    palette: {
        mode: 'dark',
    },
});

const App: React.FC = () => {
    return (
        <ThemeProvider theme={darkTheme}>
            <div id='App'>
                <header>
                    <img src='logo.png' alt='logo' width='300' />
                </header>
                <AddGameForm />
            </div>
        </ThemeProvider>
    );
};

export default App;
