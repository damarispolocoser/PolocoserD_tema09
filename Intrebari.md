1. Texturi cu sau fără transparență:
Fără transparență → obiectul este complet acoperit.
Cu transparență → zonele transparente devin invizibile; trebuie activat blending.

2. Formate de imagine pentru texturi în OpenGL:
PNG, JPG, BMP, TGA, GIF, HDR, DDS.
În OpenGL se folosesc de obicei GL_RGB sau GL_RGBA.

3. Modificarea culorii obiectului:
Dacă modul este MODULATE, culoarea obiectului se combină cu textura (poate închide sau schimba nuanța).
Dacă modul este REPLACE, textura se afișează exact ca în fișier.

4. Iluminare activată vs dezactivată:
Dezactivată → textura apare uniform, fără umbre sau efecte de lumină.
Activată → textura este afectată de lumină, umbre și reflexii, pare mai realistă.
