import { useCallback, useEffect, useState } from 'react'
import { useNavigate } from 'react-router-dom';
import { useGeneralContext } from '../../context/GeneralContext';
import ModalCreateClase from './components/create/ModalCreateClase.jsx';
import useAxiosGet from './hooks/useAxiosGet';
import { AddButton, MainContainer } from '../alumnos/styles/Styles';
import StyleComponentBreadcrumb from '../../components/StyleComponentBreadcrumb';
import { AiOutlinePlus } from 'react-icons/ai';
import ClasesTable from './components/table/ClasesTable';
import { useClaseContext } from './context/ClaseContext';

const Clases = () => {

    const { getToken, cancan, verifyToken, getUserMail, setColegio } = useGeneralContext();
    const [condFetch, setCondFetch] = useState(false)

    const [showModal, setShowModal] = useState(false);


    const [triggerUpdate, setTriggerUpdate] = useState(true);

    const nav = useNavigate()

    // eslint-disable-next-line no-unused-vars
    const [{ id: colegioId, nombre: colegioNombre }, loadingColegio, errorColegio, /* setColegio */] = useAxiosGet(`api/administrador/${getUserMail()}`, getToken());


    
    const handleShowModal = () => {
        setShowModal(true);
    }
    const handelCloseModal = () => {
        setShowModal(false);
    }
    const [tabla, setTabla] = useState(<div>Cargando tabla...</div>);

    useEffect(() => {
        verifyToken()
        if (!cancan("Administrador de Colegio")) {
            nav('/')
        } else {
            setCondFetch(true)
        }
    }, [cancan, verifyToken, nav])

    const {setClaseSelectedId} = useClaseContext();
    
    const doChangeClase= useCallback((data) => {
        setClaseSelectedId(data.id)
        nav(`/clases/view/${data.id}`)
    }, [nav, setClaseSelectedId])
    
    useEffect(() => {
        if (colegioId !== null) {
            setTabla(<ClasesTable condFetch={condFetch} colegioId={colegioId} getToken={getToken} doChangeClase={doChangeClase} triggerUpdate={triggerUpdate}/>);
        }else if(colegioId && colegioNombre){
            setColegio({
                "id": colegioId,
                "nombre": colegioNombre
            });
        }
        
    }, [colegioId, colegioNombre, condFetch, doChangeClase, getToken, setColegio, triggerUpdate])

    const handleChangeTable = () => {
        console.log("Creado")
        setTriggerUpdate(!triggerUpdate)
    }

    return <>
        
            <MainContainer>
                <StyleComponentBreadcrumb nombre="Clases" />

                {tabla}

                <AddButton>
                    <AiOutlinePlus size={"35px"} onClick={handleShowModal} />
                </AddButton>

                <ModalCreateClase title="Agregar Clase" handleClose={handelCloseModal} show={showModal} triggerState={handleChangeTable} />

            </MainContainer >


        <style jsx='true'>{`
                .page{
                    display: grid;
                    grid-template-rows: 5% 95%;
                    width: 100%;
                    height: 100vh;
                }
                .content{
                    width: 100%;
                    height: 100%;
                }
                
                .NavbarA{
                    width: 100%;
                    height: 100%;
                    background-color:  #F0544F;
                    display: flex;
                    background-color: #F0544F;
                }
                .NButtonForSideA{
                    width: 25%;
                    position: absolute;
                    bottom: 5px;
                    right : 5px;
                }
                .buttonNavBarA{
                    width: 100%;
                    height: 100%;
                    outline: none;
                    border: none;
                    background-color: #FFFFFF;
                    font-size: 50px;
                    color: #F0544F;
                }
                .pag{
                    outline: none;
                    border: none;
                    background-color: #FFFFFF;
                    font-size: 10px;
                    color: #F0544F;
                }

                .buttonNavBarAa{
                    outline: none;
                    border: none;
                    background-color: #FFFFFF;
                    font-size: 20px;
                    color: black;
                }
                .navbarmainAd{
                    width: 97.5%;
                    display: flex;
                    justify-content: space-between;
                }
                
                .CustomTable{
                    width: 100%;
                    border-spacing: 0px;
                }
                .CustomTable>thead>tr>th{
                    border: 1px solid black;
                    padding-left: 5px;
                }
                .CustomTable>tbody>tr>td{
                    text-align: center;
                    border: 1px solid black;
                }
                
                .container-table{
                    border: 0px !important;
                    
                }
                .btn-valid-page{
                    /*background: #FDF0D5;*/
                    margin-left:1rem;
                    /*border: 1px solid black;*/
                }
                
                .btn-valid-page:hover{
                    /*background: #FADAAA;*/
                    margin-left:1rem;
                    border: 1px solid black;
                }
                
                `
        }</style>
    </>
}

export default Clases
