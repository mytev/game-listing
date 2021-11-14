import axios from 'axios';

const BASE_URL = window.origin

export type CreateGameBody = {
    category?: string;
    title: string;
    subtitle?: string;
    description?: string;
    imageUrl?: string;
};

const create = (
    game: CreateGameBody
) => axios({
        method: 'post',
        url: `${BASE_URL}/listings`,
        data: game,
    });

export const ListingsApi = { create }