# Arquitectura de Aplicaciones Empresariales con .NET 8

Todo lo que necesitas para diseñar y construir Aplicaciones con una Arquitectura robusta, segura, confiable y escalable.. 
No es una guia definitiva, falla en la definición de principio SOLID, pero es el inicio para que comienzes a escribir codigo ordenado y escalable....

Fuente : https://www.udemy.com/course/arquitectura-aplicaciones-empresariales-con-net-core/

## Tabla de Contenidos

1. [Fundamentos Arquitectura de Aplicaciones](#fundamentos-arquitectura-de-aplicaciones)
   - [Proceso de diseño](#proceso-de-diseño)   
   - [Desacoplamiento entre Componentes](#desacoplamiento-entre-componentes)   
   - [Inyección de Dependencias e Inversión de control](#inyección-de-dependencias-e-inversión-de-control)
   - [Capas vs Niveles](#capas-vs-niveles)   
   - [Principios de Diseño Solid ](#principios-de-diseño-solid)   
   - [Principales Estilos de Arquitectura ](#principales-estilos-de-arquitectura)   
2. [Arquitectura N-Capas con Orientacion al Dominio](#arquitectura-n-capas-con-orientacion-al-dominio)
3. [Estructura del Curso](#estructura-del-curso)
4. [Materiales del Curso](#materiales-del-curso)
5. [Notas por Tema](#notas-por-tema)
6. [Recursos Adicionales](#recursos-adicionales)
7. [Conclusiones](#conclusiones)
8. [Contacto](#contacto)


## Fundamentos Arquitectura de Aplicaciones
El diseño de la arquitectura de una aplicación es el proceso por el cual se define una solución para los requisitos técnicos y operacionales del mismo

La arquitectura define que componentes conforman la aplicación, como se relacionan entre ellos, y como mediante su interacción llevan a cabo una funcionalidad especificada, cumpliendo con los criterios de calidad, como seguridad, disponibilidad, eficiencia, usabilidad, etc.

### Objetivo
Identificar los requisitos que producen un impacto en la estructura de la aplicacion y reducir los riesgos asociados con la construccion.

La arquitectura debe soportar los cambios futuros de la aplicacion, tanto del hardware y de funcionalidad demandada por los clientes.

#### Algunas preguntas a considerar:
1. En que entorno va a ser desplegado nuestra aplicacion?
2. Como va a ser el despliegue en produccion?
3. Como van a utilizar los usuarios nuestra app?
4. Que otros requisitos debe cumplir el sistema?, seguridad, rendimiento, concurrencia, configuracion, escalabilidad, interoperabilidad, etc
5. Que cambios en la arq pueden impactar a la app ahora o una ves desplegado?

### Conclusion
El objetivo final de la arquitectura es identificar los requisitos que producen un impacto en la estructura de la aplicación y reducir los riesgos asociados con la construcción. La arquitectura debe soportar los cambios futuros de la aplicación, del hardware y de funcionalidad demandada por los clientes.

- Mostrar la estructura del sistema pero ocultar los detalles
- Relizar todos los casos de uso
- Satisfacer en la medida de lo posible los intereses de los Stakeholders
- Ocuparse de los requisitos funcionales y de calidad (no funcionales)
- Determinar el tipo de aplicación a desarrollar
- Determinar los estilos arquitecturales que se usarán y principales cuestiones transversales

## Proceso de diseño
La diferencia entre un excelente proceso de diseño de Arquitectura y uno deficiente puede suponer el éxito o fracaso de nuestro Proyecto

Durante el proceso de diseño de la arquitectura se tratan y deciden temas importantes con la finalidad de crear un arquetipo o una plantilla básica de nuestra aplicación

- Qué tipo de aplicación se va a construir (Web, RIA, Escritorio, etc).
- Que estructura logica va a tener la aplicación (N-Capas, Componentes, etc).
- Que estructura física va a atener la aplicación (Cliente/Servidor, N-Tier. etc).
- Que riesgos hay que afrontar y como hacerlo (Seguridad, Rendimiento, Interoperabilidad, Mantenibilidad, etc).
- Que tecnologías vamos a usar (WCF, WPF, Web Api, Dapper, Entity Framework, .NET Core, JWT, etc)

### Requisitos previos
Para realizar de manera eficiente todo el proceso de diseño partiremos de la información que ha generado el proceso de captura de requisitos:
- Casos de uso o historias de usuario
- Requisitos funcionales y no funcionales
- Restricciones tecnológicas y de diseño en general
- Entorno de despliegue propuesto

### Etapas del proceso 
El proceso de diseño de Arquitectura es iterativo e incremental y costa de 5 etapas, este proceso se repite hasta completar el desarrollo de la aplicacion
1. Identificar los objetivos de la iteración
2. Seleccionar los casos de uso importantes
3. Realizar un esquema de la aplicacion
4. Identificar los principales riesgos
5. Crear arquitecturas candidatas

<img src="imagenes/etapas-del-proceso.png" alt="Etapas del proceso de diseño" width="300" />

## Desacoplamiento entre Componentes

El desacoplamiento debería realizarse entre todos los objetos (con lógica de ejecución y dependencias) pertenecientes a las diferentes capas, pues existen ciertas capas que se deben integrar a la aplicación de una forma desacoplada.

<img src="imagenes/desacoplamiento-entre-componentes.png" alt="desacoplamiento entre componentes" width="600" />

"Tener preparado toda la estructura de la arquitectura de la aplicación de forma desacoplada y en cualquier momento poder inyectar funcionalidad para cualquier área o grupo de objetos, no tiene por qué ser sólo entre capas diferentes. "

Las técnicas de desacoplamiento están basadas en el Principio de Inversión de Dependencias

<img src="imagenes/diagrama-componentes.png" alt="diagrama componentes" width="600" />

## Inyección de Dependencias e Inversión de control
### 1. Patrón de Inversión de Control (IoC)
Consiste en delegar a un componente o fuente externa, la función de seleccionar un tipo de implementación concreta de las dependencias de nuestras clases. En definitiva, este patrón describe técnicas para soportar una arquitectura tipo 'plug-in' donde los objetos pueden buscar instancias de otros objetos que requieren y de los cuales dependen

```Se refiere a un componente externo el cual tiene la función de seleccionar el tipo de implementación concreta de las dependencias de nuestras clases```

### 2. Patrón de Inyección de Dependencias (DI)
Es un patrón en el que se suplen objetos-dependencias a una clase en lugar de ser la propia clase quien cree los objetos-dependencias que necesita.

la forma más potente de implementar este patrón es mediante un 'Contenedor DI'. El contenedor DI intecta a cada objeto las dependencias-objetos necesarios según las relaciones o registro plasmado bien por código o en ficheros XML de configuración del Contenedor DI

```Se base en un framework externo, que basicamente se coloca la interfaz y la implementación  ```

Con .NET Core tenemos inyección de dependencia nativo ya que, básicamente este ha sido construido mediante interfaces

<img src="imagenes/inyeccion-dependencia-net.png" alt="inyeccion dependencia net" width="500" />

Vida útil de las dependencias de los servicios en .NET Core
- Transitorio (addTransient) : Se crea una nueva instancia, cada vez que se crea un servicio o son solicitadas

- Ambito (addScoped) : Se genera una nueva instancia para cada ambito. (Cada solicitud es un alcance). Dentro del ambito, el servicio es reutilizado

- Singleton (addSingleton) : Se crea solo una vez y se usa en todas partes

## Capas vs Niveles

Las Capas(Layers) se ocupan de la división lógica de componentes y funcionalidad, y no tienen en cuenta la localización física de componentes en diferentes servidores o en diferentes lugares.

Los Niveles(Tiers) se ocupan de la distribución física de componentes y funcionalidad en servidores separados, teniendo en cuenta topología de redes y localizaciones remotas.

Aunque tanto las Capas(Layers) como los Niveles(Tiers) usan conjuntos similares de nombres(Presentación, servicios, negocio y datos), es importante no confundirlos y recordar que solo los Niveles(Tiers) implican una separación física

<img src="imagenes/capas-vs-niveles.png" alt="capas vs niveles" width="600" />

## Principios de Diseño Solid

<img src="imagenes/solid.png" alt="principios solid" width="200" />

### 1. Principio de Única Responsabilidad
Una clase debe tener una unica responsabilidad o característica. Dicho de otra manera, una clase debe tener una única razón por la que está justificado realizar cambios sobre su código fuente. Una consecuencia de este principio es que, de forma general, las clases deberían tener pocas dependencias con otras clases-tipos.

<img src="imagenes/single-responsability-principle.png" alt="principio de responsabilidad unica" width="370" />

### 2. Principio Abierto Cerrado
Una clase debe estar abierta para la extensión y cerrada para la modificación. Es decir, el comportamiento de una clase debe poder ser extendido sin necesidad de realziar modificaciones sobre el código de la misma.

<img src="imagenes/open-closed-principle.png" alt="principio abierto - cerrado" width="450" />

### 3. Princio de Sustitución de Liskov
Los subtipos deben poder ser sustituibles por sus tipos base (interfaz o clase base). Este hecho se deriva de que el comportamiento de un programa que trabaja con abstracciones (interfaces o clases base) no debe cambiar porque se sustituya una implementación concreta por otra. Los programas deben hacer referencia a las abstracciones, y no a las implementaciones

<img src="imagenes/liskov-substitution-principle.png" alt="principio de sustitucion liskov" width="450" />

### 4. Principio de Segregación de Interfaces
Las implementaciones de las Interfaces en las clases no deben estar obligados a implementar métodos que no se usan. Es decir, las interfaces de clases deben ser específicos dependiendo de quién los consume y por lo tanto, tienen que estar granularizados - segregados en diferentes interfaces no debiendo crear nunca grandes interfaces

<img src="imagenes/interface-segration-principle.png" alt="principio de segregacion de interfaces" width="450" />


### 5. Principio de Inversión de Dependencias
Las abstracciones no deben depender de los detalles - Los detalles deben depender de las abstracciones. Las dependencias directas entre clases deben ser reemplazadas por abstracciones(interfaces) para permitir diseños top-down sin requerir primero el diseño de los niveles inferiores.

<img src="imagenes/dependency-inversion-principle.png" alt="principio de inversion de dependencias" width="450" />

## Otros principios claves

### El diseño de componentes debe ser altamente cohesivo
No sobrecargar los componentes añadiendo funcionalidad mezclada o no relacionada. Por ejemplo, evitar mezclar lógica de acceso a datos con lógica de negocio perteneciente al Modelo del Dominio

### Mantener el código transversal abstraído de la lógica específica de la aplicación
El código transversal se refiere a código de aspectos horizontales, cosas como la seguridad, gestión de operaciones, logging, instrumentalización, etc.

### Separación de Preocupaciones-Responsabilidades
Divir la aplicación en distintas partes minimizando las funcionalidades superpuestas entre dichas partes. El factor fundamental es minimizar los puntos de interacción para conseguir una alta cohesión y un bajo acoplamiento

### No repetirse(DRY)
Se debe especificar 'la intención' en un único sitio en el sistema. Por ejemplo, en términos del diseño de una aplicación, una funcionalidad específica se debe implementar en un único componente; esta misma funcionalidad no debe estar implementada en otros componentes

## Principales Estilos de Arquitectura
Un estilo de arquitectura es una familia de arquitecturas que comparten determinadas características

### N-Niveles

<img src="imagenes/n-niveles.png" alt="n-niveles" width="250" />

Es una arquitectura tradicional para aplicaciones empresariales. Las dependencias se administran mediante la división de la aplicación en capas que realizan funciones lódicas como presentaciones, lógica de negocios y acceso a datos. Una capa solo puede llamar a las capas que se encuentran por debajo de ella


### Web-Cola-Trabajo

<img src="imagenes/web-cola-trabajo.png" alt="web-cola-trabajo" width="300" />

En este estilo, la aplicación tiene un Front-End web que controla las solicitudes HTTP y un trabajo de back-end que realiza tareas de uso intensivo de la CPU u operaciones de larga duración. El Front-End se comunica con el trabajo a través de una cola de mensajes asincrónicos.

La arquitectura Web-Cola-Trabajo es adecuada para dominios relativamente sencillos con algunas tareas que consumen muchos recursos


### Microservicios

<img src="imagenes/microservices.png" alt="microservices" width="300" />

Una aplicación de microservicios se compone de muchos servicios pequeños e independientes. Cada servicio implementa una sola función empresarial. Los servicios están acoplados de forma flexible y se comunican a través de contratos de API

Una arquitectura de microservicios es más compleja a la hora de compilar y administrar 

### CQRS

<img src="imagenes/cqrs.png" alt="cqrs" width="300" />

El estilo CQRS, separa las operaciones de lectura y escritura en modelos independientes. Esto permite aislar las partes del sistema que actualizan los datos de las partes que leen los datos.


### Arquitectura Basa en Eventos

<img src="imagenes/eventos.png" alt="eventos" width="300" />

Usa un modelo de publicacion-suscripcion en el que los productores publican eventos y los consumidores se suscriben a ellos. Los productores son independientes de los consumidores y estos, a su vez. Son independientes entre si. Se deberia tenerlo en cuenta para aplicaciones que consumen y procesan un gran volumen de datos con una latencia muy baja

## Arquitectura N-Capas con Orientacion al Dominio

### Definicion 
El objetivo de esta arquitectura marco es proporcionar una base consolidada para un tipo concreto de aplicaciones: "Aplicaciones empresariales complejas"

<img src="imagenes/ddd-layered-architecture.png" alt="ddd-layered-architecture" width="600" />

### Diseño de un microservicio DDD
El diseño guiado por el dominio(DDD) propone un modelado basado en la realidad de negocio con relacion a sus casos de uso

En el contexto de la creación de aplicaciones, DDD hace referencia a los problemas como dominios. Describe áreas con problemas independientes como contextos delimitados y resalda un lenguaje comun para hablar de dichos problemas

### Niveles en microservicios de DDD
1. Nivel de aplicacion: Este nivel debe mantenerse estrecho. No contiene reglas de negocios ni conocimientos, sino que solo coordina tareas y delega trabajo a colaboraciones de objetos de dominio en el siguiente nivel
2. Nivel del Modelado del Dominio: Responsable de representar conceptos del negocio, información sobre la situación del negocio y reglas de negocio
3. Nivel de Infraestructura: Es la forma en que los datos inicialmente se conservan en las entidades de dominio se guardan en base de datos o en otro almacen
