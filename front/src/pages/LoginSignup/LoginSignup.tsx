import { useEffect, useState } from 'react'
import { Container, Button, Form } from 'react-bootstrap'
import { useNavigate } from 'react-router-dom'
import './LoginSignup.css'
import { authService } from '../../services/authService'

type Props = {}

export default function LoginSignup({ }: Props) {

    const navigate = useNavigate();
    const [loginState, setLoginState] = useState<boolean>(false)
    const [email, setEmail] = useState<string>('')
    const [password, setPassword] = useState<string>('')
    const [errors, setErrors] = useState<{ email?: string, password?: string, general?: string }>({})

    useEffect(() => {
        if (loginState) {
            navigate('/login')
        }
        else {
            navigate('/signup')
        }
    }, [loginState, navigate])

    const validateEmail = (email: string) => {
        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/
        return emailRegex.test(email)
    }

    const validateForm = () => {
        let formErrors: { email?: string, password?: string } = {}

        if (!email) {
            formErrors.email = 'Email is required'
        } else if (!validateEmail(email)) {
            formErrors.email = 'Invalid email format'
        }

        if (!password) {
            formErrors.password = 'Password is required'
        } else if (password.length < 6) {
            formErrors.password = 'Password must be at least 6 characters long'
        }

        setErrors(formErrors)
        return Object.keys(formErrors).length === 0
    }

    const handleSubmit = (event: React.FormEvent) => {
        event.preventDefault()
        if (validateForm()) {
            console.log('Form submitted:', { email, password })
        }
    }

    const handleLogin = async () => {
        if (!loginState) {
            setLoginState(true)
            setEmail('')
            setPassword('')
            setErrors({})
        } else {
            try {
                const response = await authService.Login({ email, password });
                console.log('Login successful', response);
                localStorage.setItem('token', response.token);
                navigate('/');
            } catch (error) {
                console.error('Login error', error);
                setErrors({ general: 'Invalid email or password' });  // Обрабатываем ошибки
            }
        }
    }

    const handleSignUp = async () => {
        if (loginState) {
            setLoginState(false)
            setEmail('')
            setPassword('')
            setErrors({})
        } else {
            if (validateForm()) {
                try {
                    const response = await authService.Register({ email, password });  // Используем authService для регистрации
                    console.log('Registration successful', response);
                    navigate('/login');  // После успешной регистрации перенаправляем на страницу входа
                } catch (error) {
                    console.error('Registration error', error);
                    setErrors({ general: 'Registration failed' });  // Обрабатываем ошибки
                }
            }
        }
    }

    return (
        <Container fluid style={{ height: '100vh', display: 'flex', justifyContent: 'center', alignItems: 'center', backgroundColor: '#F3F9FF' }}>
            <Form onSubmit={handleSubmit}
                style={{
                    border: '1px solid #E3E2E0',
                    borderRadius: '10px',
                    height: '500px',
                    display: 'flex',
                    justifyContent: 'center',
                    alignItems: 'center',
                    flexDirection: 'column',
                    backgroundColor: 'white',
                    boxShadow: '0 0px 8px rgba(0, 0, 0, 0.1)',
                }}
            >
                {loginState ? <h1 style={{ fontSize: '50px' }}>Login</h1> : <h1 style={{ fontSize: '50px' }}>Sign Up</h1>}
                <Form.Group style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', margin: '35px 40px 20px 40px', width: '500px', backgroundColor: '#eaeaea', height: '70px', borderRadius: '5px' }}>
                    <svg style={{ margin: '0 10px', width: '22px', flexShrink: 0 }} fill='grey' xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512">
                        <path d="M48 64C21.5 64 0 85.5 0 112c0 15.1 7.1 29.3 19.2 38.4L236.8 313.6c11.4 8.5 27 8.5 38.4 0L492.8 150.4c12.1-9.1 19.2-23.3 19.2-38.4c0-26.5-21.5-48-48-48L48 64zM0 176L0 384c0 35.3 28.7 64 64 64l384 0c35.3 0 64-28.7 64-64l0-208L294.4 339.2c-22.8 17.1-54 17.1-76.8 0L0 176z" />
                    </svg>
                    <Form.Control className={`${errors.email ? 'is-invalid' : ''} no-effects`} style={{ marginRight: '10px', border: 'none', outline: 'none', backgroundColor: '#eaeaea', fontSize: '22px' }} value={email} onChange={(event) => setEmail(event.target.value)} type='email' placeholder='Enter email' />
                    {errors.email && <div className="invalid-feedback" style={{ fontSize: '20px' }}>{errors.email}</div>}
                </Form.Group>
                <Form.Group style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', width: '500px', backgroundColor: '#eaeaea', height: '70px', borderRadius: '5px' }}>
                    <svg style={{ margin: '0 10px', width: '20px', flexShrink: 0 }} fill='grey' xmlns="http://www.w3.org/2000/svg" viewBox="0 0 448 512">
                        <path d="M144 144l0 48 160 0 0-48c0-44.2-35.8-80-80-80s-80 35.8-80 80zM80 192l0-48C80 64.5 144.5 0 224 0s144 64.5 144 144l0 48 16 0c35.3 0 64 28.7 64 64l0 192c0 35.3-28.7 64-64 64L64 512c-35.3 0-64-28.7-64-64L0 256c0-35.3 28.7-64 64-64l16 0z" />
                    </svg>
                    <Form.Control className={`${errors.password ? 'is-invalid' : ''} no-effects`} style={{ marginRight: '10px', border: 'none', outline: 'none', backgroundColor: '#eaeaea', fontSize: '22px' }} type='password' value={password} placeholder='Enter password' onChange={(e) => setPassword(e.target.value)} />
                    {errors.password && <div className="invalid-feedback" style={{ fontSize: '20px' }}>{errors.password}</div>}
                </Form.Group>
                {loginState && <Form.Text style={{ marginTop: '10px', fontSize: '20px' }}>Forgor password? <span>Click here!</span></Form.Text>}
                {loginState ?
                    <Form.Group style={{ display: 'flex', justifyContent: 'space-between', width: '400px', marginTop: '30px' }}>
                        <Button onClick={handleSignUp} variant='outline-secondary' style={{ width: '160px', borderRadius: '50px', fontSize: '25px' }}>Sign Up</Button>
                        <Button type='submit' onClick={handleLogin} style={{ width: '160px', borderRadius: '50px', fontSize: '25px' }}>Login</Button>
                    </Form.Group>
                    :
                    <Form.Group style={{ display: 'flex', justifyContent: 'space-between', width: '400px', marginTop: '30px' }}>
                        <Button onClick={handleLogin} variant='outline-secondary' style={{ width: '160px', borderRadius: '50px', fontSize: '25px' }}>Login</Button>
                        <Button type='submit' onClick={handleSignUp} style={{ width: '160px', borderRadius: '50px', fontSize: '25px' }}>Sign Up</Button>
                    </Form.Group>
                }
            </Form>
        </Container>
    )
}