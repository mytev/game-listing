import { Alert, Button } from '@mui/material';
import Box from '@mui/material/Box';
import TextField from '@mui/material/TextField';
import React, { useEffect, useState } from 'react';
import validator from 'validator';
import { ListingsApi } from './listing.api';

export const AddGameForm: React.FC = () => {
    const [category, setCategory] = useState('');
    const [title, setTitle] = useState('');
    const [subtitle, setSubtitle] = useState('');
    const [description, setDescription] = useState('');
    const [imageUrl, setImageUrl] = useState('');

    const [isTitleInvalid, setIsTitleInvalid] = useState(false);
    const [isImageURLInvalid, setIsImageURLInvalid] = useState(false);

    const [toastState, setToastState] = useState('');

    return (
        <>
            <Toast
                toastState={toastState}
                onDismiss={() => setToastState('')}
            />
            <Box
                id="AddGameForm"
                component="form"
                sx={{
                    display: 'flex',
                    flexDirection: 'column',
                    '> div': { m: 1.4, width: '500px' },
                }}
            >
                <TextField
                    id="category"
                    label="Category"
                    onChange={(e) => setCategory(e.target.value)}
                />
                <TextField
                    id="title"
                    label="Title"
                    required
                    onChange={(e) => setTitle(e.target.value)}
                    onBlur={() => setIsTitleInvalid(!title)}
                    error={isTitleInvalid}
                    helperText={isTitleInvalid ? 'Title cannot be empty' : null}
                />
                <TextField
                    id="subtitle"
                    label="Subtitle"
                    onChange={(e) => setSubtitle(e.target.value)}
                />
                <TextField
                    id="description"
                    label="Description"
                    multiline
                    rows={4}
                    onChange={(e) => setDescription(e.target.value)}
                />
                <TextField
                    id="imageUrl"
                    label="Image URL"
                    onChange={(e) => setImageUrl(e.target.value)}
                    onBlur={() => setIsImageURLInvalid(!isValidURL(imageUrl))}
                    error={isImageURLInvalid}
                    helperText={isImageURLInvalid ? 'Invalid URL' : null}
                />
            </Box>
            <Button
                variant="outlined"
                sx={{
                    m: 3,
                    width: '100px',
                }}
                disabled={isTitleInvalid || isImageURLInvalid}
                onClick={() =>
                    ListingsApi.create({
                        category,
                        title,
                        subtitle,
                        description,
                        imageUrl,
                    })
                        .then(() => setToastState('success'))
                        .catch(() => setToastState('error'))
                }
            >
                Submit
            </Button>
        </>
    );
};

const isValidURL = (val: string) => {
    if (!val) {
        return true;
    }
    return validator.isURL(val);
};

const Toast: React.FC<{ toastState: string; onDismiss?: () => void }> = ({
    toastState,
    onDismiss,
}) => {
    useCallDismissAfterMs(2000);
    if (!toastState) {
        return null;
    }

    const isError = toastState === 'error';
    return (
        <Alert
            variant="outlined"
            severity={isError ? 'error' : 'success'}
            sx={{ marginBottom: 1 }}
        >
            {isError
                ? 'Oops, something went wrong...'
                : 'Game tile added successfully'}
        </Alert>
    );

    function useCallDismissAfterMs(ms: number) {
        useEffect(() => {
            let timeout: NodeJS.Timeout;
            if (toastState && onDismiss) {
                timeout = setTimeout(onDismiss, ms);
            }
            return () => clearTimeout(timeout);
        }, [toastState, onDismiss]);
    }
};
