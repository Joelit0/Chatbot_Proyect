# Proyecto 2022 - Programación 2_
PRIMERA ENTREGA:
Comenzamos haciendo las tarjetas CRC (utilizamos la página CRC Card Maker) para darle forma al proyecto y tener un punto de inicio; definimos las clases, sus responsabilidades y colaboradores, esto nos ayudó a tener una base sobre la cual trabajar y buscar mejorar.
Al hacer el diagrama UML (utilizamos la aplicación draw.io) comenzamos a dudar acerca de las relaciones de ciertas clases y los métodos de algunas. Un gran dilema que se creo fue si hacer cuatro Boards por partida, cada jugador tendría dos tableros; uno de defensa y otro de ataque, para posicionar sus barcos y los ataques al tablero enemigo, pero llegamos a la conclusion de que no era necesario, se crearía un tablero por jugador y se envia una copia del mismo al oponente, solo que el oponente no puede ver la posicion de los barcos, a menos que emboque a uno. Este cambio nos ayuda a evitar la repeticion innecesaria de informacion.

No implementamos la funcionalidad de jugar contra la IA, debido a que nos falta la información del bot a esta altura del proyecto.
Los recursos utilizados para esta primera entrega fueron principalmente los conceptos brindados por los profesores, consultas que hicimos en clase y también utilizamos el libro Object Design que está en la bibliografía del curso para hacer el informe de los roles de cada clase.

|SEGUNDA ENTREGA|
Para esta entrega nos organizamos usando la herramienta Trello, en la anterior también lo habíamos hecho pero esta vez fuimos más consistentes para mantener el flujo de trabajo, fuimos más organizados y buscamos dividir tareas equitativamente 
Link de trello: https://trello.com/invite/b/ySMd3vZr/1457885ee6835b61e8ebd9db6ec27ec5/proyecto-programacion-2
Enfrentamos algunas adversidades ya que priorizamos cosas como el Bot y terminamos con poco tiempo para otras cosas, pero igualm

En el Program.cs dejamos un ejemplo de como está funcionando el juego. Luego, debajo de ese fragmento de código dejamos comentado todo lo relacionado al Bot. Ambas cosas funcionan pero no están conectadas aún, por lo tanto, si se quiere probar el Bot deben de comentar el fragmento de código que dice "Lógica del juego" y descomentar el fragmento de código que dice "Lógica del Bot".

Id de Telegram del bot en caso de que quieran probarlo = @SuperBattleship_bot.

|ENTREGA FINAL|
Para esta entrega centramos nuestros esfuerzos en dejar nuestro proyecto lo más funcional y sólido posible, lamentablemente la falta de tiempo nos forzó a renunciar a la implementación de excepciones
así como de la funcionalidad de Timer para poner limites de tiempo globales y entre rondas a las partidas. Sin embargo apreciamos el soporte de los profesores para ayudarnos a llegar a un estandar de
calidad que nosotros consideramos bueno en nuestro proyecto, este semestre nos ha hecho mejorar como 
individuos y como equipo.

