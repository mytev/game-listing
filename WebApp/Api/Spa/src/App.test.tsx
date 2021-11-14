import { render } from '@testing-library/react';
import App from './App';

describe('<App />', () => {
    it('Given When render Then do not crash', () => {
        render(<App />);
    });
});
