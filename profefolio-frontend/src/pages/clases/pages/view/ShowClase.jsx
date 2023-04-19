import React from 'react'
import { useParams } from 'react-router-dom';

const ShowClase = (props) => {
    const { idClase } = useParams();

    return <>
        <div>User Profile for User ID: {idClase}</div>
    </>

}

export default ShowClase