import { useEffect, useState } from 'react'
import { useNavigate } from 'react-router-dom';
import { useGeneralContext } from '../../context/GeneralContext';
import ModalCreateClase from './components/create/ModalCreateClase.jsx';
import useAxiosGet from './hooks/useAxiosGet';
import { AddButton, MainContainer } from '../alumnos/styles/Styles';
import StyleComponentBreadcrumb from '../../components/StyleComponentBreadcrumb';
import { AiOutlinePlus } from 'react-icons/ai';
import ClasesTable from './components/table/ClasesTable';



const Clases = () => {

    const { getToken, cancan, verifyToken, getUserMail } = useGeneralContext();
    const [condFetch, setCondFetch] = useState(false)

    const [showModal, setShowModal] = useState(false);

    const nav = useNavigate()

    // eslint-disable-next-line no-unused-vars
    const [{ id: colegioId, nombre: colegioNombre }, loadingColegio, errorColegio, setColegio] = useAxiosGet(`api/administrador/${getUserMail()}`, getToken());


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

    useEffect(() => {
        if (colegioId !== null) {
            setTabla(<ClasesTable condFetch={condFetch} colegioId={colegioId} getToken={getToken} doChangeStudent={doChangeStudent} />);
        }
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [colegioId, condFetch, getToken])




    const doChangeStudent = (data) => {
        console.log("Seleccionado", data)
        nav(`/clases/view/${data.id}`)
    }

    return <>
        <MainContainer>
            <StyleComponentBreadcrumb nombre="Clases" />

            {tabla}

            <AddButton>
                <AiOutlinePlus size={"35px"} onClick={handleShowModal} />
            </AddButton>

            <ModalCreateClase title="Agregar Clase" handleClose={handelCloseModal} show={showModal}>
            </ModalCreateClase>

        </MainContainer >
    </>
}

export default Clases
