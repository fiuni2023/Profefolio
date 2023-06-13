import React, { useEffect, useState } from 'react'
import { useModularContext } from '../../context';
import { useGeneralContext } from '../../../../context/GeneralContext';
import { toast } from 'react-hot-toast';
import APILINK from '../../../../components/link';
import axios from "axios";
const DetalleAlumno=()=>{
    const { dataSet, stateController} = useModularContext()
    const {materiaId}=stateController;
    const {alumnoId, infoAlumno}=dataSet;
    const [datosAlumno, setDatosAlumno]=useState([]);
    const { getToken } = useGeneralContext();
    console.log(infoAlumno)

    return <>
    <h1>Hola</h1>
    </>

}
export default DetalleAlumno