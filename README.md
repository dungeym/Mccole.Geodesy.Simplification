# Mccole.Geodesy.Simplification

A library of filters that can, when given a series of points along a line (known as a polyline), filter those points to reduce their number but retain a line that follows a similar curve.

I used several references for this project but the best by far was the work of [Elmar de Koning](http://psimpl.sourceforge.net/index.html).  This project is not a C# port of Elmar's C++ work, it's an implementation of his documentation but given the impact Elmar's work had on this project I've licensed it under the same terms of the [MPL](https://www.mozilla.org/en-US/MPL/) license.

### Algorithms

[**Douglas-Peucker**](http://psimpl.sourceforge.net/douglas-peucker.html)

[**Lang**](http://psimpl.sourceforge.net/lang.html)

[**Nth Point**](http://psimpl.sourceforge.net/nth-point.html)

[**Opheim**](http://psimpl.sourceforge.net/opheim.html)

[**Radial Distance**](http://psimpl.sourceforge.net/radial-distance.html)

[**Perpendicular Distance**](http://psimpl.sourceforge.net/perpendicular-distance.html)

[**Reumann-Witkam**](http://psimpl.sourceforge.net/reumann-witkam.html)
