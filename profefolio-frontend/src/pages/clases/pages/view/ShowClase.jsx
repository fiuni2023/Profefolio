import React from 'react'
import { useParams } from 'react-router-dom';

const ShowClase = (props) => {
    //const { userId } = props.match.params;
    const par = useParams();
    console.log(par)
    return <>
        <div>User Profile for User ID: {5}</div>
    </>

}

export default ShowClase