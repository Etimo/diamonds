package se.etimo.jsontools;

import com.fasterxml.jackson.databind.ObjectMapper;

import java.util.Iterator;
import java.util.Spliterator;
import java.util.Spliterators;
import java.util.stream.Stream;
import java.util.stream.StreamSupport;

public class Json {
    public static final ObjectMapper JSON_MAPPER = new ObjectMapper();
    public static <T> Stream<T> getStream(Iterator<T> iterator){
        return StreamSupport.<T>stream(
        Spliterators.spliteratorUnknownSize(iterator, Spliterator.ORDERED),false);
    }
}
